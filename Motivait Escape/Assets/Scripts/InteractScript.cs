using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class InteractScript : MonoBehaviour
{
    private GameObject player;
    private Text UIText;
    private bool ObjectSet;
    void Start()
    {
        player = transform.gameObject;
        UIText = transform.GetComponentInChildren<Text>();
        ObjectSet = false;
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
                    var ObjectScript = hit.transform.gameObject.GetComponent<InteractionObject>();
                    UIText.text = "This item is: " + ObjectScript.Name;
                }
            }
         //   Debug.DrawRay(cam.position, cam.forward * 10, Color.red);
        }
       
    }

    void SetInteractionObject(GameObject player)
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

    public void RemoveInteractionObject(GameObject value)
    {
        value.GetComponent<MeshRenderer>().material = null;
        value.GetComponent<MeshFilter>().mesh = null;
    }

}
