using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            var cam = Camera.main.transform;
            if (Physics.Raycast(cam.position, cam.forward, out hit, 10))
            {
                if (hit.transform.gameObject.tag == "Interactable")
                {
                    var script = hit.transform.gameObject.GetComponent<InteractionObject>();
                    script.SetInteractionObject(transform.gameObject);
                    
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
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
