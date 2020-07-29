using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorInteraction : MonoBehaviour
{
    private bool canOpen;
    private bool isDoorOpen;
    public bool isLocked;
    public string KeyRequired;
    private string DoorSideOpen;

    private static GameObject Player;
    private Animator animController;

    public GameObject DoorFront, DoorBack;

    // Start is called before the first frame update
    void Start()
    {
        canOpen = true;
        animController = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    public void OpenDoor()
    {
        if (canOpen)
        {
            if (isDoorOpen)
            {
                CloseDoor();
                return;
            }

            if (CheckDoorSide() == "Front")
            {
                animController.SetBool("OpenFront", true);
                StartCoroutine(DoorCooldown("OpenFront"));
            }
            else
            {
                animController.SetBool("OpenBack", true);
                StartCoroutine(DoorCooldown("OpenBack"));
            }
            isDoorOpen = true;
        }
      
    }

    void CloseDoor()
    {
        animController.SetBool("OpenFront", false);
        animController.SetBool("OpenBack", false);
        if (DoorSideOpen == "Front")
        {
            animController.SetBool("CloseDoorFront", true);
            StartCoroutine(DoorCooldown("CloseDoorFront"));
        }
        else
        {
            animController.SetBool("CloseDoorBack", true);
            StartCoroutine(DoorCooldown("CloseDoorBack"));
        }
        isDoorOpen = false;
      
    }

    IEnumerator DoorCooldown(string AnimBoolName)
    {
        canOpen = false;
        yield return new WaitForSeconds(1.1f);
        animController.SetBool(AnimBoolName, false);
        canOpen = true;

    }

    string CheckDoorSide()
    {
        float DoorDistance;
        DoorSideOpen = "Front";
        DoorDistance = Vector3.Distance(Player.transform.position, DoorFront.transform.position);
        Debug.Log("The distance of the front is: " + DoorDistance);

        float BackDistance = Vector3.Distance(Player.transform.position, DoorBack.transform.position);
        Debug.Log("The distance of the back is: " + BackDistance);
        if (DoorDistance > BackDistance)
            DoorSideOpen = "Back";
       
        return DoorSideOpen;
    }


    public void UnlockDoor(CharacterMovement controller)
    {
        if (controller.CheckForKey(KeyRequired)) //Checks the player list for the string name of the key required
        {
            Debug.Log("The door is unlocked");
            isLocked = false;
        }
        else
            Debug.Log("The door is still locked find another key");
    }
}
