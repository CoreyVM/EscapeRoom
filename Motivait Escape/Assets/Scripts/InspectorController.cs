using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorController : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement playerScript;
    private float rotationSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.transform.gameObject.GetComponent<CharacterMovement>();

    }
    void Update()
    {
        if (player != null && playerScript.GetIsInspecting())
        {
            ZoomObject();
            RotateObject();
        }
    }

    private void ZoomObject()
    {

    }

    private void RotateObject()
    {
        float xRot = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float yRot = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;

        transform.Rotate(yRot, xRot, 0,Space.Self);

    }
}
