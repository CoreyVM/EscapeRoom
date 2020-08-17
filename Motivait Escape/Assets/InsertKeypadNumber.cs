using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertKeypadNumber : MonoBehaviour
{
    [SerializeField] private int KeypadValueIndex;
    [SerializeField] private CharacterMovement playerRef;


    public void RevealKeypadValue()
    {
        playerRef.SetCombinationNumberVisible(KeypadValueIndex);
    }

}
