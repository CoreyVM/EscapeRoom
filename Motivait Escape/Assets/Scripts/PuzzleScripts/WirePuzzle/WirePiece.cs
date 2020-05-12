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
        canMove = true;
        SetPiecePosition();
    }

    void SetPiecePosition()
    {
        switch (BlockPosition)
        {
            case 1:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(-90, 0, 0);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(-180, 0, 0);
                break;
            case 4:
                transform.rotation = Quaternion.Euler(-270, 0, 0);
                break;
        }
     
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
            BlockPosition++;
            if (BlockPosition > 4)
                BlockPosition = 1;

            Debug.Log(this.BlockPosition);
            SetPiecePosition();
        }
    }

}
