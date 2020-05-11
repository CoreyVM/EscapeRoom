using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 10f; //Components
    private Rigidbody rigid;
    private GameObject hitObject;
    public Text UIText;
    public GameObject InspectingObject;

    private static bool isInspecting; //Member Variables
    private List<string> KeysFound = new List<string>(); 

   public List<string> GetKeysFound() { return KeysFound; }

    public void SetIsInspecting() { isInspecting = !isInspecting; }
    public bool GetIsInspecting() { return isInspecting; }
  
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
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
        if (Input.GetKeyDown(KeyCode.F) && !isInspecting) //Interacts with the object 
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

}
