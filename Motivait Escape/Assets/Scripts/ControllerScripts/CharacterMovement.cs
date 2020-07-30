using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 10f; //Components
    private Rigidbody rigid;
    private GameObject hitObject;
    private Camera PlayerCamera;
    public Text UIText;
    public GameObject InspectingObject;

    public Canvas OptionScreen;

    private bool isInspecting, PickedUp; //Member Variables
    private List<string> KeysFound = new List<string>();

    private GameObject pickedUpObject;
    public void SetPickedUpObject(GameObject value) { pickedUpObject = value; }
    public void SetPickedUp(bool value) { PickedUp = value; }
    public bool GetPickedUp() { return PickedUp; }

    public GameObject GetPickedUpObject() { return pickedUpObject; }
   public List<string> GetKeysFound() { return KeysFound; }
    public GameObject GetHitObject()
    {
        return hitObject;
    }
    public void SetIsInspecting(bool value) { isInspecting = value; }
    public bool GetIsInspecting() { return isInspecting; }

    public void SetCameraEnabled(bool value) { PlayerCamera.enabled = value; }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        PlayerCamera = GetComponentInChildren<Camera>();
        KeysFound.Clear();
        ShowOptionScreen(false);
    }
    void Update()
    {
        InteractWithObject();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionScreen.enabled)
            {
                ShowOptionScreen(false);
                
            }
            else
                ShowOptionScreen(true);
        }
    }

    private void FixedUpdate()
    {
        if (!isInspecting)
        {
            Move();
            if (pickedUpObject != null)
            {
                MovePickedUpObject();
            }
        }
    }

    private void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, yAxis) * moveSpeed;

        Vector3 newPosition = rigid.position + rigid.transform.TransformDirection(movement) * Time.deltaTime;
        rigid.MovePosition(newPosition);  
    }

    private void InteractWithObject()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isInspecting) //Interacts with the object if not already interacting
        {
            RaycastHit hit;
            var cam = Camera.main.transform;
            if (Physics.Raycast(cam.position, cam.forward, out hit, 10))
            {
                if (hit.transform.gameObject.tag == "Interactable" && !isInspecting)
                {
                    hitObject = hit.transform.gameObject;
                    var script = hit.transform.gameObject.GetComponent<InteractionObject>();
                    script.InteractWithItem();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.F) && isInspecting) //IF the player is interacting with an object
        {
            if (hitObject != null)
            {
                var script = hitObject.transform.gameObject.GetComponent<InteractionObject>();
                if (script)
                {
                    script.InteractWithItem();
                }
            }
        }
    }

    public void AddKey(string Name)
    {
        KeysFound.Add(Name);
        Debug.Log(KeysFound[0]);
    }

    public void RemoveKey(string Name)
    {
        KeysFound.Remove(Name);
    }

    public bool CheckForKey(string Name)
    {
        if (KeysFound.Contains(Name))
            return true;
        else
            return false;
    }

    void MovePickedUpObject()
    {
        var rigid = pickedUpObject.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.isKinematic = true;
        pickedUpObject.transform.position = InspectingObject.transform.position;
        pickedUpObject.transform.rotation = InspectingObject.transform.rotation;
        pickedUpObject.transform.parent = InspectingObject.transform.parent;
    }

    void ShowOptionScreen(bool value)
    {
        OptionScreen.enabled = value;
        isInspecting = value;
    }
}
