using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private bool isLocked;
    public string KeyRequired;


    // Start is called before the first frame update
    void Start()
    {
        isLocked = true; 
    }

    public void OpenDoor()
    {
        if (!isLocked)
        {
            //Enter open door logic here (animations)
        }
    }

    public void UnlockDoor(CharacterMovement controller)
    {
        if (controller.CheckForKey(KeyRequired))
        {
            Debug.Log("The door is unlocked");
            isLocked = false;
        }
        else
            Debug.Log("The door is still locked find another key");
    }

}
