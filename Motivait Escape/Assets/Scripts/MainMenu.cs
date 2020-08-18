using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void StartGame()
    {
        player.SetActive(true);
        Cursor.visible = false;
        this.transform.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ProtoypeScene");
    }
}
