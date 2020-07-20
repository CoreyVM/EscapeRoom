﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public enum ObjectType {
    None, Puzzle, Door,
    Inspectable, Interactable, Key, PC, Light, Screen, Slide, Printer, Screwdriver};

public class InteractionObject : MonoBehaviour
{
    public string ItemName, ItemDescription, PuzzleType;
    public ObjectType ItemType;
    public Mesh ObjectMesh;
    private Renderer renderer;

    private Vector3 Scale, Rotation;
    public bool canRead;

    public Vector3 GetScale() { return Scale; }
    public Vector3 GetRotation() { return Rotation; }

    protected void Start()
    {
        Scale = transform.localScale;
        Rotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        renderer = this.transform.gameObject.GetComponent<Renderer>();
    }

   public void SetInteractionObject(GameObject player)
    {
       var PlayerMeshFilter = player.GetComponentInChildren<MeshFilter>();
       var PlayerMeshRenderer = player.GetComponentInChildren<MeshRenderer>();
        
       PlayerMeshFilter.sharedMesh = this.transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
       PlayerMeshRenderer.sharedMaterial = this.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
       player.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);

    }
    public void RemoveInteractionObject(GameObject value)
    {
        value.GetComponent<MeshRenderer>().material = null;
        value.GetComponent<MeshFilter>().mesh = null;
        this.SetObjectVisiblity(true);
    }

    public void InteractWithItem(CharacterMovement controller)
    {
        switch (ItemType)
        {
            case ObjectType.None:
                break;
            case ObjectType.Inspectable:
                InspectObject(controller);
                break;
            case ObjectType.Interactable:
                if (!controller.GetPickedUp())
                {
                    PickUpObject(controller);
                    break;
                }
                else
                {
                    var rigid = controller.GetPickedUpObject().GetComponent<Rigidbody>();
                    rigid.useGravity = true;
                    rigid.isKinematic = false;
                    rigid.AddForce(Camera.main.transform.forward * 500);
                    rigid.transform.parent = null;
                    controller.SetPickedUp(false);
                    controller.SetPickedUpObject(null);
                    break;
                }  
            case ObjectType.Key:
                controller.AddKey(ItemName);
                Destroy(this.gameObject);
                break;
            case ObjectType.PC:
                break;
            case ObjectType.Puzzle:
                switch (PuzzleType)
                {
                    case "Internet Puzzle":
                        var Board = GameObject.FindGameObjectWithTag("WireBoard");
                        var BoardScript = Board.GetComponent<PuzzleBoard>();
                        if (!controller.GetIsInspecting())
                        {
                            controller.SetIsInspecting(true);
                            controller.SetCameraEnabled(false);
                            BoardScript.SetCameraEnabled(true);
                            BoardScript.SetPlayerScript(controller);
                            break;
                        }
                        else
                        {
                            controller.SetIsInspecting(false);
                            controller.SetCameraEnabled(true);
                            BoardScript.SetCameraEnabled(false);
                            BoardScript.SetPlayerScript(null);
                        }
                        
                        break;
                    case "Keypad Puzzle":
                        var KeyPad = GameObject.FindGameObjectWithTag("Keypad");
                        var KeyPadScript = KeyPad.GetComponent<KeypadBoard>();
                        if (!controller.GetIsInspecting())
                        {
                            controller.SetIsInspecting(true);
                            controller.SetCameraEnabled(false);
                            KeyPadScript.SetCameraEnabled(true);
                            KeyPadScript.SetPlayerScript(controller);
                            KeyPad.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;
                            break;

                        }
                        else
                        {
                            controller.SetIsInspecting(false);
                            controller.SetCameraEnabled(true);
                            KeyPadScript.SetCameraEnabled(false);
                            KeyPadScript.SetPlayerScript(null);
                            KeyPad.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true;
                            break;
                        }
                }
                break;
            case ObjectType.Door:
                var DoorScript = this.transform.gameObject.GetComponent<DoorInteraction>();
                if (!DoorScript.isLocked)
                {
                    DoorScript.OpenDoor();
                    break;
                }
                else if (controller.GetKeysFound().Capacity > 0)
                    DoorScript.UnlockDoor(controller);
                break;
            case ObjectType.Light:
                var LightSwitchScript = controller.GetHitObject().GetComponent<TurnLight>();
                LightSwitchScript.ToggleLight();
                break;
            case ObjectType.Screen:
                var ScreenScript = controller.GetHitObject().GetComponent<Screen>();
                ScreenScript.InteractiveScreen();
                break;
            case ObjectType.Slide:
                var SlideScript = controller.GetHitObject().GetComponent<ProjectorClick>();
                SlideScript.ToggleSlide();
                break;
            case ObjectType.Printer:
                var PrinterScript = controller.GetHitObject().GetComponent<PrinterInteraction>();
                PrinterScript.Interact();
                break;
            case ObjectType.Screwdriver:
                Debug.Log("Newcastle");
                var screwdriverScript = controller.GetHitObject().GetComponent<ScrewdriverInteraction>();
                screwdriverScript.Interact();
                break;
        }
    }

    public void SetObjectVisiblity(bool value)
    {
        renderer.enabled = value;
    }

    private void InspectObject(CharacterMovement controller)
    {
        if (!controller.GetIsInspecting())
        {
            controller.SetIsInspecting(true);
            controller.UIText.text = "";
            var inspector = controller.InspectingObject.GetComponent<InspectorController>();
            inspector.SetCanRead(this.canRead);
            inspector.SetObjectScript(this);
            this.SetInteractionObject(controller.InspectingObject);
            SetObjectVisiblity(false);
        }
        else
        {
            controller.SetIsInspecting(false);
            this.RemoveInteractionObject(controller.InspectingObject);
            controller.InspectingObject.GetComponent<InspectorController>().ResetValues();
        }
        //else if(controller.GetIsInspecting())
        //{
        //    controller.SetIsInspecting(false);
        //}
        

      
    }

    public void PickUpObject(CharacterMovement controller)
    {
        if (!controller.GetPickedUp())
        {
            controller.SetPickedUpObject(this.transform.gameObject);
            controller.SetPickedUp(true);
        }
        else
        {
            controller.SetPickedUp(false);
            controller.SetPickedUpObject(null);
        }
    }
}
