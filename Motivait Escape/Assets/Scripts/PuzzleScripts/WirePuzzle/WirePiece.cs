using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class WirePiece : MonoBehaviour
{
    public enum PieceType { Straight, Corner};
    public PieceType BlockType;
 
    bool isActive, canMove;
    public int BlockPosition;

    private static PuzzleBoard BoardScript;
    public int WinPosition;

    public void SetBlockActive(bool value) { isActive = value; }
    public bool GetIsActive() { return isActive; }
    public void SetCanMove(bool value) { canMove = value; }

    public int GetBlockPosition() { return BlockPosition; }

    void Start()
    {
        canMove = true;
        SetPiecePosition();
        BoardScript = GetComponentInParent<PuzzleBoard>();
    }

    public bool CheckWinPosition()
    {
        bool inPosition = false;
        switch (BlockType)
        {
            case PieceType.Straight:
                inPosition = CheckStraightPosition();
                break;
            case PieceType.Corner:
                inPosition = CheckCornerPosition();
                break;
        }
        return inPosition;
    }

    bool CheckStraightPosition()
    {
        if (WinPosition == 1 && BlockPosition == 1 || WinPosition == 1 && BlockPosition == 3)
            return true;
        else if (WinPosition == 2 && BlockPosition == 2 || WinPosition == 2 && BlockPosition == 4 )
            return true;
     
        return false;
    }

    bool CheckCornerPosition()
    {
        if (BlockPosition == WinPosition)
            return true;
        else
            return false;
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
    private void OnMouseDown()
    {
        if (canMove)
        {
            BlockPosition++;
            if (BlockPosition > 4)
                BlockPosition = 1;

        //    Debug.Log(this.BlockPosition);
            SetPiecePosition();
            BoardScript.CheckForConnection();
            
        }
    }

}
