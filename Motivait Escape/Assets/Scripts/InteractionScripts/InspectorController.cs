using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class InspectorController : MonoBehaviour
{
    private GameObject parent;
    private GameObject player;
    private CharacterMovement playerScript;
    private float rotationSpeed = 150;

    private bool canRead, TextSet;
    public string ItemDescription;

    public void SetCanRead(bool value) { canRead = value; }
    private InteractionObject objectScript;

    public void SetObjectScript(InteractionObject script) { objectScript = script; }

    public Text UIText;

    void Start()
    {
        TextSet = false;
        parent = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.transform.gameObject.GetComponent<CharacterMovement>();
        canRead = false;
    }
    void Update()
    {
        if (player != null && playerScript.GetIsInspecting())
        {
            ZoomObject();
            RotateObject();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canRead)
        {
            SetUIText();
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

    public void ResetValues()
    {
        this.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
        this.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        parent.transform.localPosition = new UnityEngine.Vector3(0, 0, 1.26f);
        TextSet = false;
        UIText.text = "";
    }

    public void SetObjectScale(InteractionObject item)
    {
        UnityEngine.Vector3 scale = item.transform.localScale;
        this.transform.localScale = scale;
    }

    private void SetUIText() 
    {
        TextSet = !TextSet; //Creates a bool toggle so the text can be closed
        if (TextSet)
        {
            UIText.text = objectScript.ItemDescription;
        }
        else
        {
            UIText.text = "";
        }
    }
}
