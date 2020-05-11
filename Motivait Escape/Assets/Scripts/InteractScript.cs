using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class InteractScript : MonoBehaviour
{
    private GameObject player, hitObject;
    private Text UIText;
    private bool TextSet;
    void Start()
    {
        player = transform.gameObject;
        UIText = transform.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (player != null)
        {
            RaycastHit hit;
            var cam = Camera.main.transform;
            if (Physics.Raycast(cam.position,cam.forward, out hit, 10))
            {
      
                if (hit.transform.gameObject.tag == "Interactable")
                {
                    hitObject = hit.transform.gameObject;
                    var ObjectScript = hit.transform.gameObject.GetComponent<InteractionObject>();
                    hitObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.1f);
              //      Debug.Log("Hit the object");
                    if (!TextSet)
                    {
                        UIText.text = "This item is: " + ObjectScript.Name;
                        TextSet = true;
                    }
                }
                else
                {
                    UIText.text = "";
                    TextSet = false;
                    if (hit.transform.gameObject != hitObject && hitObject != null)
                    {
                        hitObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0);
                        hitObject = null;
                        Debug.Log("Set the width to nothing");
                    }
                }
             
            }
        }
       
    }

    void SetInteractionObject(GameObject player)
    {
        var PlayerMeshFilter = player.GetComponentInChildren<MeshFilter>();
        var PlayerMeshRenderer = player.GetComponentInChildren<MeshRenderer>();

        PlayerMeshFilter.sharedMesh = this.transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
        PlayerMeshRenderer.sharedMaterial = this.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        TextSet = false;
    }     

    public void RemoveInteractionObject(GameObject value)
    {
        value.GetComponent<MeshRenderer>().material = null;
        value.GetComponent<MeshFilter>().mesh = null;
    }

}
