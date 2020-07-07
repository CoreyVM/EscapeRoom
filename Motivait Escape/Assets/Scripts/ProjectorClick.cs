using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectorClick : MonoBehaviour
{
    public RawImage Slide;

    public bool on = false;
    public void ToggleSlide()
    {
        if (!on)
        {
            on = true;
            Slide.enabled = true;
        }

        else
        {
            on = false;
            Slide.enabled = false;
        }
    }
}
