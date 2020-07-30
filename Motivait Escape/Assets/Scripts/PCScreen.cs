using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCScreen : MonoBehaviour
{
    public GameObject ActiveCanvas;
    public CharacterMovement playerRef;
    private bool IsInteracting = false;
    public void InteractiveScreen()
    {
        IsInteracting = !IsInteracting;
        ActiveCanvas.SetActive(IsInteracting);
        playerRef.SetIsInspecting(IsInteracting);
        Cursor.visible = IsInteracting;
    }
}