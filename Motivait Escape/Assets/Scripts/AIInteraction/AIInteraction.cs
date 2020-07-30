using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIInteraction : MonoBehaviour
{
    public string HelperText;
    public Text UIText;
    private IEnumerator courutine;


    public void SetHelpText()
    {
        courutine = ResetUIText();
        UIText.text = HelperText;
        StartCoroutine(courutine);
    }

    IEnumerator ResetUIText()
    {
        yield return new WaitForSeconds(5);
        UIText.text = "";
    }
}
