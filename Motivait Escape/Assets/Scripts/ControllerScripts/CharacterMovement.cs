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


    private bool isInspecting, PickedUp; //Member Variables
    private List<string> KeysFound = new List<string>();

    private GameObject pickedUpObject;

   public List<string> GetKeysFound() { return KeysFound; }


    public void SetIsInspecting() { isInspecting = !isInspecting; }
    public bool GetIsInspecting() { return isInspecting; }

    public void SetCameraEnabled(bool value) { PlayerCamera.enabled = value; }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        PlayerCamera = GetComponentInChildren<Camera>();
        KeysFound.Clear();
    }
    void Update()
    {
        InteractWithObject();
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
        if (Input.GetKeyDown(KeyCode.F) && !isInspecting && PlayerCamera.enabled == true) //Interacts with the object 
        {
            RaycastHit hit;
            var cam = Camera.main.transform;
            if (Physics.Raycast(cam.position, cam.forward, out hit, 10))
            {
                if (hit.transform.gameObject.tag == "Interactable" && !isInspecting)
                {
                    hitObject = hit.transform.gameObject;
                    var script = hit.transform.gameObject.GetComponent<InteractionObject>();
                    script.InteractWithItem(this);
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.F) && isInspecting)
        {
            var script = hitObject.transform.gameObject.GetComponent<InteractionObject>();
            script.RemoveInteractionObject(InspectingObject);
            isInspecting = false;
            InspectingObject.GetComponent<InspectorController>().ResetValues();
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

        if (Input.GetKey(KeyCode.Q))
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
            rigid.AddForce(Camera.main.transform.forward * 1000);
            pickedUpObject.transform.parent = null;
            pickedUpObject = null;
            Debug.Log("Get yeeted");
        }
        else
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;
            pickedUpObject.transform.position = InspectingObject.transform.position;
            pickedUpObject.transform.rotation = InspectingObject.transform.rotation;
            pickedUpObject.transform.parent = InspectingObject.transform.parent;
        }



    }

}
