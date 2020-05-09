using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {
    None, Puzzle,
    Inspectable, PC };

public class InteractionObject : MonoBehaviour
{
    public string Name;
    public ObjectType Type;
    public Mesh ObjectMesh;

    private bool ObjectSet;

    private void Start()
    {
        ObjectSet = false;
    }

   private void Update()
    {
        
    }

   public void SetInteractionObject(GameObject player)
    {
        if (!ObjectSet)
        {
            var PlayerMeshFilter = player.GetComponentInChildren<MeshFilter>();
            var PlayerMeshRenderer = player.GetComponentInChildren<MeshRenderer>();

            PlayerMeshFilter.sharedMesh = this.transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
            PlayerMeshRenderer.sharedMaterial = this.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterial;


            ObjectSet = true;
        }
    }
   
}
