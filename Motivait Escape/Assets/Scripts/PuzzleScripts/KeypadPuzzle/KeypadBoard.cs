using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadBoard : MonoBehaviour
{
    public KeypadKey[] Keypads;
    private int[] Solution = { 2, 4, 1, 3 }; //Use an array as we dont need to adjust in runtime
    private List<int> UserSolution = new List<int>(); //Use a list as we need to clear/add values in runtime
    private int KeysPressedIndex = 0;

    private CharacterMovement playerScript;
    public Camera KeypadCamera;

    public void SetPlayerScript(CharacterMovement script) { playerScript = script; }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            if (playerScript != null)
            {
                if (playerScript.GetIsInspecting())
                    playerScript.SetIsInspecting(false);
              
                playerScript.SetCameraEnabled(true);
                SetCameraEnabled(false);
                ResetKeypad();
                this.transform.parent.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }


    public void SetCameraEnabled(bool value)
    {
        KeypadCamera.enabled = value;
        if (value == true)
            Cursor.visible = true;
        else
            Cursor.visible = false;

        foreach (KeypadKey obj in Keypads)
        {
            obj.transform.gameObject.SetActive(value);
        }
    }

    void CheckForSolution()
    {
        bool isCorrect = false;
        int SolutionValue, UserValue;
        for (int i = 0; i < 4; i++)
        {
            SolutionValue = Solution[i];
            UserValue = UserSolution[i];
            if (UserValue == SolutionValue)
                isCorrect = true;
            else
            {
                isCorrect = false;
                break;
            }       
        }

        if (!isCorrect)
            ResetKeypad();
        else
            Debug.Log("You win");
    }

    public void IncrementKeyPressed() 
    {
        ++KeysPressedIndex;
    }

    public void CheckForCompleteCombination()
    {
        if (KeysPressedIndex >= 4)
            CheckForSolution();
    }

    public void InsertKeyCombination(int value) {
        UserSolution.Add(value); 
    }

    private void ResetKeypad()
    {
        for (int i = 0; i < Keypads.Length; i++) {
            if (Keypads[i].GetKeyPressed())
            {
                Debug.Log(i);
                Keypads[i].SetKeyPressed(false);
                Keypads[i].ResetAnimation();
            //    Keypads[i].transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white); //Remove this when we have models with animations
            }
        }
        UserSolution.Clear();
        KeysPressedIndex = 0;
    }
    
}
