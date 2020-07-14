using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
public class HighscoreTable : MonoBehaviour
{
    public Text playerTime;
    public Text highscoreText;
    public Timer gameTimer;

    private string highscoreMinutes = "05";
    private string highscoreSeconds = "30";
    private int playerMinutes;
    private int playerSeconds;

    private void Start()
    {
        string value = PlayerPrefs.GetString("HighscoreMinutes", highscoreMinutes) +  " : " + PlayerPrefs.GetString("HighscoreSeconds", highscoreSeconds);
        GetPlayerFinalTime();
        highscoreText.text = value;
       
    }

    private void GetPlayerFinalTime()
    {
        float seconds = float.Parse(gameTimer.GetSeconds());
        int minutes = int.Parse(gameTimer.GetMinutes());
        playerTime.text = minutes.ToString() + " : " + seconds.ToString();
   //     CheckForNewHighScore();
    }

    void CheckForNewHighScore()
    {
        float Highseconds = float.Parse(PlayerPrefs.GetString("HighscoreSeconds", highscoreSeconds));
        int Highminutes = int.Parse(PlayerPrefs.GetString("HighscoreMinutes", highscoreMinutes));



        if (Highminutes >= playerMinutes)
        {
            if (Highseconds > playerSeconds)
            {
                Debug.Log("The highscore will stay the samew");
            }
        }
      
  
    }
}


