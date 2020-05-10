using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody rigid;
    private GameObject hitObject;
    private static bool isInspecting;
    public GameObject InspectingObject;
    public void SetIsInspecting(bool value) { isInspecting = value; }
    public bool GetIsInspecting() { return isInspecting; }
    
    
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isInspecting)
        {
            RaycastHit hit;
            var cam = Camera.main.transform;
            if (Physics.Raycast(cam.position, cam.forward, out hit, 10))
            {
                if (hit.transform.gameObject.tag == "Interactable" && !isInspecting)
                {
                    var script = hit.transform.gameObject.GetComponent<InteractionObject>();
                    script.SetInteractionObject(InspectingObject);
                    hitObject = hit.transform.gameObject;
                    isInspecting = true;
                    Debug.Log("The mesh has been copied");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.F) && isInspecting)
        {
            var script = transform.gameObject.GetComponent<InteractScript>();
            script.RemoveInteractionObject(InspectingObject);
            isInspecting = false;
            InspectingObject.GetComponent<InspectorController>().ResetRotation();
          
        }

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
}
