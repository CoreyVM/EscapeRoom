using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCDesktopScript : MonoBehaviour
{
    public RawImage DesktopImage;
    private bool isOpen;

   public void ToggleImage()
    {
        isOpen = !isOpen;
        DesktopImage.enabled = isOpen;
    }
}
