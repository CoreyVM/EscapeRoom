using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    private List<GameObject> boardPieces = new List<GameObject>();
    private GameObject StartingBlock, EndingBlock;
    public Camera puzzleCam;
    private bool hasWon = false;
    private CharacterMovement playerScript;

    int[] SolutionIndexes = { 0, 5, 10, 11, 12, 7, 2, 3, 8, 9, 14 };
  
    public void SetPlayerScript(CharacterMovement script) { playerScript = script; }

    int currentIndex = 0;

    public bool GetHasWon() { return hasWon; }

    void Start()
    {
        puzzleCam.enabled = false;
        var pieces = GameObject.FindGameObjectsWithTag("WirePiece");
        foreach (GameObject obj in pieces)
        {
            obj.SetActive(false);
            boardPieces.Add(obj);
        }
        StartingBlock = boardPieces[0];
        EndingBlock = boardPieces[boardPieces.Count - 1]; //Gets the first and last index of the board


        boardPieces[0].GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);



        //So we can track the wire connection
        StartingBlock.GetComponent<WirePiece>().SetCanMove(false);
        EndingBlock.GetComponent<WirePiece>().SetCanMove(false);//Makes these block pre determined position 
                                                                //so these blocks cant be moved by the player

        CheckForConnection();
    }

    void Update()
    {
        if (playerScript != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                if (playerScript.GetIsInspecting())
                {
                    playerScript.SetIsInspecting(false);
                    playerScript.SetCameraEnabled(true);
                    SetCameraEnabled(false);
                }
                else
                    playerScript.SetIsInspecting(true);
            }
        }
    }

    public void SetCameraEnabled(bool value)
    {
        transform.GetComponent<BoxCollider>().enabled = value;
        puzzleCam.enabled = value;

        if (value == true)
            Cursor.visible = true;
        else
            Cursor.visible = false;

        foreach (GameObject obj in boardPieces)
            obj.SetActive(value);
    }

    public void CheckForConnection()
    {
        bool hasBroken = false;
 
        for (int i = 0; i < SolutionIndexes.Length; i++)
        {
            var Block = boardPieces[SolutionIndexes[i]].GetComponent<WirePiece>();
            var PropertyBlock = new MaterialPropertyBlock();
            PropertyBlock.SetColor("_BaseColor", Color.yellow);
            if (Block.CheckWinPosition() && !hasBroken)
                Block.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
            else
            {
                PropertyBlock.SetColor("_BaseColor", Color.white);
                Block.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
                hasBroken = true;
            }
        }

        if (!hasBroken)
            hasWon = true;
        else
            hasWon = false;
    }


}
