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
    private bool hasLoggedOn = false; //Use this for the typing text funciton (log in to your pc)
    public List<Texture2D> PCTextures;
    public RawImage previewImage;


    [SerializeField]
    private string PCPassword;
    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private GameObject LogInButton;
    [SerializeField]
    private GameObject NotepadIcon;
    [SerializeField]
    private GameObject InternetIcon;
    [SerializeField]
    private Text infoText;


    private void Start()
    {
        previewImage.texture = PCTextures[0];
        NotepadIcon.SetActive(false);
        InternetIcon.SetActive(false);
    }

    public void InteractiveScreen()
    {
        IsInteracting = !IsInteracting;
        ActiveCanvas.SetActive(IsInteracting);
        playerRef.SetIsInspecting(IsInteracting);
        Cursor.visible = IsInteracting;
    }

    bool CheckForInternetAccess()
    {
        if (!wirePuzzleRef.GetHasWon())
            return false;
        return true;
    }

   public void CheckPassword()
    {
        if (!CheckForInternetAccess())
        {
            SetInformationText("Can't connect to login services.", true);
            return;
        }
        else
            SetInformationText("", false);

        if (inputField.text == PCPassword)
            ShowDesktopScreen();
        else
            StartCoroutine(WrongPassword());
    }

    void ShowDesktopScreen()
    {
        previewImage.texture = PCTextures[1];
        ActiveCanvas.GetComponentInChildren<RawImage>().texture = PCTextures[1];
        inputField.transform.gameObject.SetActive(false);
        LogInButton.SetActive(false);
        NotepadIcon.SetActive(true);
        InternetIcon.SetActive(true);
    }

    IEnumerator WrongPassword()
    {
        SetInformationText("That password was wrong, try again.", true);
        yield return new WaitForSeconds(5f);
        SetInformationText("", false);
    }

    void SetInformationText(string text, bool visiblity)
    {
        infoText.text = text;
        infoText.enabled = visiblity;
    }
}