using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadKey : MonoBehaviour
{
    public KeypadBoard boardScript;
    public int KeyPadValue;
    private bool KeyPressed = false;

    public void SetKeyPressed(bool value) { KeyPressed = value; }
    public bool GetKeyPressed() { return KeyPressed; }


    private void OnMouseDown()
    {
        if (!KeyPressed && boardScript != null)
        {
            Debug.Log("I have pressed the key");
            KeyPressed = true;
            this.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow); //Remove this when we have models with animations
            boardScript.InsertKeyCombination(KeyPadValue);
            boardScript.IncrementKeyPressed();
        }
        else
            Debug.Log("The key didnt work");
    }
}
