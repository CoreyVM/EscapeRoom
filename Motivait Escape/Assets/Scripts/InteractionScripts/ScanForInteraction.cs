using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class ScanForInteraction : MonoBehaviour
{
    private GameObject player, hitObject;
    private Text UIText;
    private bool TextSet;
    private Camera PlayerCamera;
    void Start()
    {
        player = transform.gameObject;
        UIText = transform.GetComponentInChildren<Text>();
        PlayerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (player != null && PlayerCamera.enabled == true)
        {
            RaycastHit hit;
            var cam = Camera.main.transform;
            if (Physics.Raycast(cam.position,cam.forward, out hit, 10)) //Checks for an interaction item for description/highlighting
            {
                if (hit.transform.gameObject.tag == "Interactable")
                {
                    hitObject = hit.transform.gameObject;
                    var ObjectScript = hit.transform.gameObject.GetComponent<InteractionObject>();
                    hitObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.1f);                 
                }
                else
                {
                    ////UIText.text = "";
                    //TextSet = false;
                    if (hit.transform.gameObject != hitObject && hitObject != null)
                    {
                        hitObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0);
                        hitObject = null;
                        
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



}
