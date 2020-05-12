using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePiece : MonoBehaviour
{
    bool isActive, canMove;
    public int BlockPosition;

    public void SetBlockActive(bool value) { isActive = value; }
    public void SetCanMove(bool value) { canMove = value; }

    void Start()
    {
        
    }

    void CheckForConnection()
    {
        if (isActive)
        {

        }
    }

    private void OnMouseDown()
    {
        if (canMove)
        {
            Debug.Log("Rotate me ");
        }
    }

}
