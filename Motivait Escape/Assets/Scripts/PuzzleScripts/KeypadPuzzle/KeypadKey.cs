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

    private Animation animator;

    private void Start()
    {
        animator = GetComponent<Animation>();
    }

    private void OnMouseDown()
    {
        if (!KeyPressed && boardScript != null)
        {
            KeyPressed = true;
            //   this.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow); //Remove this when we have models with animations
            boardScript.IncrementKeyPressed();
            boardScript.InsertKeyCombination(KeyPadValue);
            animator.Play("ButtonPressed" + ParseGameObjectNumericalValue());
        }
    }

    string ParseGameObjectNumericalValue()
    {
        string ButtonName = this.transform.gameObject.name;
        string Value = ButtonName.Substring(12);
        return Value;
    }

    public void ResetAnimation()
    {
        animator.Play("ButtonReleased" + ParseGameObjectNumericalValue());
    }

    public void AddNumberToSolution()
    {
        boardScript.CheckForCompleteCombination();
    }
}
