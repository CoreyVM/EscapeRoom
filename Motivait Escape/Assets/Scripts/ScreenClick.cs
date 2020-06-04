using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenClick : MonoBehaviour
{   
    public Image image;
  
    public bool on = false;
    public void ToggleScreen()
    {
        if (!on)
        {
            on = true;
            image.enabled = false;
        }

        else
        {
            on = false;
            image.enabled = !image.enabled;
        }
    }
 /*   void Update()
    {
        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        image.transform.position = namePose;
    }
 */
}