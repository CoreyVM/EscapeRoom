using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngineInternal;

public class InteractScript : MonoBehaviour
{
    private GameObject player, hitObject;
    [SerializeField] private CharacterMovement playerScript;
    private Text UIText;
    private bool TextSet;


    void Start()
    {
        player = transform.gameObject;
 //       UIText = transform.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (player != null && !playerScript.GetIsInspecting())
        {
            RaycastHit hit;
            var cam = Camera.main.transform;
            if (Physics.Raycast(cam.position, cam.forward, out hit, 10))
            {
                if (hitObject != null && hit.transform.gameObject != hitObject)
                {
                    hitObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0);
                    hitObject = null;
                }

                if (hit.transform.gameObject != null)
                {
                    if (hit.transform.gameObject.tag == "Interactable")
                    {
                        hitObject = hit.transform.gameObject;
                        var ObjectScript = hit.transform.gameObject.GetComponent<InteractionObject>();
                        hitObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.08f);
                //        hitObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                   //     Debug.Log("Set colour top something");
                  //      var temp = 
   
                    }
                    else
                    {
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

    }

}


