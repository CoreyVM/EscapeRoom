using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    private List<GameObject> boardPieces = new List<GameObject>();
    private GameObject StartingBlock, EndingBlock;
    public Camera puzzleCam;

    private CharacterMovement playerScript;

    public void SetPlayerScript(CharacterMovement script) { playerScript = script; }

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
                                                          //So we can track the wire connection
        StartingBlock.GetComponent<WirePiece>().SetCanMove(false);
        EndingBlock.GetComponent<WirePiece>().SetCanMove(false);//Makes these block pre determined position 
                                                                //so these blocks cant be moved by the player
    }

    void Update()
    {
        if (playerScript != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                if (playerScript.GetIsInspecting())
                {
                    playerScript.SetIsInspecting();
                    playerScript.SetCameraEnabled(true);
                    SetCameraEnabled(false);
                }
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
}
