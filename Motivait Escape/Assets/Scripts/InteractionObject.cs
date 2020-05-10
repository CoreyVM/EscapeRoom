using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {
    None, Puzzle,
    Inspectable, PC };

public class InteractionObject : MonoBehaviour
{
    public string Name, ItemDescription;
    public ObjectType Type;
    public Mesh ObjectMesh;

    private Vector3 Scale, Rotation;
    public bool canRead;

    public Vector3 GetScale() { return Scale; }
    public Vector3 GetRotation() { return Rotation; }

    private void Start()
    {
        Scale = transform.localScale;
        Rotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

   public void SetInteractionObject(GameObject player)
    {
       var PlayerMeshFilter = player.GetComponentInChildren<MeshFilter>();
       var PlayerMeshRenderer = player.GetComponentInChildren<MeshRenderer>();
        
       PlayerMeshFilter.sharedMesh = this.transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
       PlayerMeshRenderer.sharedMaterial = this.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
    }

   
}
