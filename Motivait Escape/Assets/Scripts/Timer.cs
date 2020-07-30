using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    private float startTime;

    public bool isActive;
    public string CurrentTimeString;
    private float CurrentTime;

    private string seconds;
    private string minutes;
    public string GetSeconds() { return seconds; }
    public string GetMinutes() { return minutes; }
    public float GetCurrentTime() { return CurrentTime; }


    void Start()
    {
        isActive = true;
        startTime = Time.time;
    }

    void Update()
    {
        if (isActive)
        {
            CurrentTime = Time.time - startTime;
            minutes = ((int)CurrentTime / 60).ToString();
            seconds = (CurrentTime % 60).ToString("f2");
            CurrentTimeString = minutes + " : " + seconds;

            TimerText.text = CurrentTimeString;
        }
    }

    public void Finish()
    {
        TimerText.color = Color.yellow;
    }
}
