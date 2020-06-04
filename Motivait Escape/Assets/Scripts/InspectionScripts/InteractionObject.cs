using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ObjectType {
    None, Puzzle, Door,
    Inspectable, Key, PC, Light };

public class InteractionObject : MonoBehaviour
{
    public string ItemName, ItemDescription;
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
            case ObjectType.Key:
                controller.AddKey(ItemName);
                Destroy(this.gameObject);
                break;
            case ObjectType.PC:
                break;
            case ObjectType.Puzzle:
                break;
            case ObjectType.Door:
                if (controller.GetKeysFound().Capacity > 0)
                    this.transform.gameObject.GetComponent<DoorInteraction>().UnlockDoor(controller);
                break;
            case ObjectType.Light:
                var LightSwitchScript = controller.GetHitObject().GetComponent<TurnLight>();
                LightSwitchScript.ToggleLight();
                break;
        }
    }

    public void SetObjectVisiblity(bool value)
    {
        renderer.enabled = value;
    }

    private void InspectObject(CharacterMovement controller)
    {
        controller.SetIsInspecting();
        controller.UIText.text = "";
        var inspector = controller.InspectingObject.GetComponent<InspectorController>();
        inspector.SetCanRead(this.canRead);
        inspector.SetObjectScript(this);
        this.SetInteractionObject(controller.InspectingObject);
        SetObjectVisiblity(false);
    }
}
