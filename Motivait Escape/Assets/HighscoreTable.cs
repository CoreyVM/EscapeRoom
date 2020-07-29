using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighscoreTable : MonoBehaviour
{
    public Text playerTime;
    public Text highscoreText;
    public Timer gameTimer;
    public Text NewScoreSetText;

    private string highscoreMinutes = "05";
    private string highscoreSeconds = "30";
    private int playerMinutes;
    private float playerSeconds;

    private void Start()
    {
        string value = PlayerPrefs.GetString("HighscoreMinutes", highscoreMinutes) +  " : " + PlayerPrefs.GetString("HighscoreSeconds", highscoreSeconds);
        GetPlayerFinalTime();
        highscoreText.text = value;

    }

    private void GetPlayerFinalTime()
    {
       playerSeconds = float.Parse(gameTimer.GetSeconds());
        playerMinutes = int.Parse(gameTimer.GetMinutes());
       playerTime.text = playerMinutes.ToString() + " : " + playerSeconds.ToString();
        CheckForNewHighScore();
    }

    void CheckForNewHighScore()
    {
        float Highseconds = float.Parse(PlayerPrefs.GetString("HighscoreSeconds", highscoreSeconds));
        int Highminutes = int.Parse(PlayerPrefs.GetString("HighscoreMinutes", highscoreMinutes));

        if (playerMinutes > Highminutes)
            SetNewHighScore();
        
        else if(playerMinutes == Highminutes)
        {
            if (playerSeconds > Highseconds)
            {
                SetNewHighScore();
            }
        }
      
  
    }

    void SetNewHighScore()
    {
        PlayerPrefs.SetString("HighscoreSeconds", playerSeconds.ToString());
        PlayerPrefs.SetString("HighscoreMinutes", playerMinutes.ToString());
        highscoreMinutes = playerMinutes.ToString();
        highscoreSeconds = playerSeconds.ToString();
        highscoreText.text = highscoreMinutes + " : " + highscoreSeconds;
        StartCoroutine(ShowWinText());
    }

    IEnumerator ShowWinText()
    {
        NewScoreSetText.enabled = true;
        yield return new WaitForSeconds(5);
        NewScoreSetText.enabled = false;
    }

    private void RevertHighScore()
    {
        PlayerPrefs.DeleteKey("HighscoreSeconds");
        PlayerPrefs.DeleteKey("HighscoreMinutes");
    }
}


