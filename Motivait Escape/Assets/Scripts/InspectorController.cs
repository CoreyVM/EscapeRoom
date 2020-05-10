using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class InspectorController : MonoBehaviour
{
    private GameObject parent;
    private GameObject player;
    private CharacterMovement playerScript;
    private float rotationSpeed = 150;


    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.transform.gameObject.GetComponent<CharacterMovement>();
       
    }
    void Update()
    {
        if (player != null && playerScript.GetIsInspecting())
        {
            ZoomObject();
            RotateObject();

            Debug.Log(parent.transform.localPosition.z);

        }
    }

    private void ZoomObject()
    {
       float zPos = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed * Time.deltaTime;
       parent.transform.Translate(UnityEngine.Vector3.forward * zPos);

        UnityEngine.Vector3 clampedPos = parent.transform.localPosition;
        clampedPos.z = Mathf.Clamp(clampedPos.z, 1, 3);
        parent.transform.localPosition = clampedPos;
        
    }

    private void RotateObject()
    {
        float xRot = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float yRot = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;

        this.transform.Rotate(yRot, xRot, 0,Space.Self);
    }

    public void ResetRotation()
    {
        this.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
        parent.transform.localPosition = new UnityEngine.Vector3(0, 0, 1.26f);
     // Add in the reset to z position in world scale for the parent objects
    }
}
