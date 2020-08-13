using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCDesktopScript : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement playerRef;

    public RawImage DesktopImage;
    public RawImage WebsiteImage;

    private bool isOpen;
    private bool SetNumberTextVisible = false;

   public void ToggleImage()
    {
        if (!SetNumberTextVisible)
        {
            SetNumberTextVisible = true;
            playerRef.SetCombinationNumberVisible(1);
        }
        isOpen = !isOpen;
        DesktopImage.enabled = isOpen;
    }
    public void ToggleInternetImage()
    { 
        isOpen = !isOpen;
        WebsiteImage.enabled = isOpen;
    }
}
