using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    public GameObject ActiveCanvas;
    public CharacterMovement playerRef;
    public bool IsInteracting = false;
    public void InteractiveScreen()
    {
        if (!IsInteracting)
        {
            IsInteracting = true;
            ActiveCanvas.SetActive(true);
            playerRef.SetIsInspecting(true);
            Cursor.visible = true;
        }
        else
        {
            IsInteracting = false;
            ActiveCanvas.SetActive(false);
            playerRef.SetIsInspecting(false);
            Cursor.visible = false;
        }
    }
}