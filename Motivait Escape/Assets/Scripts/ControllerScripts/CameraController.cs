using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    private float Smoothing = 2f;

    private GameObject player;
    private Vector2 smoothVelocity;
    private Vector2 currentRotation;

    private PostProcessOutline outline;

    CharacterMovement controlScript;

    void Start()
    {
        player = transform.parent.gameObject;
        controlScript = transform.parent.gameObject.GetComponent<CharacterMovement>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    } 
    void Update()
    {
        if (!controlScript.GetIsInspecting())
        {
            RotateCamera();
        }
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputValues = Vector2.Scale(inputValues, new Vector2(mouseSensitivity * Smoothing, mouseSensitivity * Smoothing));

        smoothVelocity.x = Mathf.Lerp(smoothVelocity.x, inputValues.x, 1f / Smoothing);
        smoothVelocity.y = Mathf.Lerp(smoothVelocity.y, inputValues.y, 1f / Smoothing);

        currentRotation += smoothVelocity;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-currentRotation.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(currentRotation.x, player.transform.up);
    }
}
