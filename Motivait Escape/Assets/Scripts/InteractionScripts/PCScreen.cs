using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCScreen : MonoBehaviour
{
    public GameObject ActiveCanvas;
    public CharacterMovement playerRef;
    public PuzzleBoard wirePuzzleRef;
    private bool IsInteracting = false;
    private bool hasLoggedOn = false; //Use this for the tpying text funciton (log in to your pc)

    public void InteractiveScreen()
    {
        IsInteracting = !IsInteracting;
        ActiveCanvas.SetActive(IsInteracting);
        playerRef.SetIsInspecting(IsInteracting);
        Cursor.visible = IsInteracting;

        if (!CheckForInternetAccess())
        {
            Debug.Log("there is no internet access cant log in"); //Place log in function here
        }
    }

    bool CheckForInternetAccess()
    {
        if (!wirePuzzleRef.GetHasWon())
            return false;
        return true;
    }
}