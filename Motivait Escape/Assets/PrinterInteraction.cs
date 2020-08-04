using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterInteraction : MonoBehaviour
{
    public CharacterMovement PlayerRef;
    public GameObject Paper;
    private bool isJammed;
    void Start()
    {
        isJammed = true;
    }

    public void Interact()
    {
        if (isJammed && PlayerRef.hasScrewdriver)
            Paper.SetActive(true);
    }
}
