using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);



        /*
        highscoreEntryList = new List<HighscoreEntry>() {
            new HighscoreEntry{score = 1234, name= "ABC" },
         new HighscoreEntry{score = 1335, name= "CBA" },
          new HighscoreEntry{score = 123784, name= "KMS" },
           new HighscoreEntry{score = 1212334, name= "ABC" },
           new HighscoreEntry{score = 1242434, name= "ABC" },
         new HighscoreEntry{score = 133123235, name= "CBA" },
          new HighscoreEntry{score = 14, name= "KMS" },
           new HighscoreEntry{score = 12534, name= "ABC" },
                 new HighscoreEntry{score = 1784, name= "KMS" },
           new HighscoreEntry{score = 12134, name= "ABC" },
        };*/

        string jsonString = PlayerPrefs.GetString("highscoreTable");
       Highscores highscores  = JsonUtility.FromJson<Highscores>(jsonString);

        //Sorting ALogirthm
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
          
            }        

        }
        
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry entry in highscores.highscoreEntryList)
                CreateHighscoreEntryTransform(entry, entryContainer, highscoreEntryTransformList);


      //  //Save the leaderboard
      ////  Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
      //  string json = JsonUtility.ToJson(highscores);
      //  PlayerPrefs.SetString("highscoreTable", json);
      //  PlayerPrefs.Save();
      //  Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }

  

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {

        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
            case 4: rankString = "4th"; break;
            case 5: rankString = "5th"; break;
            case 6: rankString = "6th"; break;
            case 7: rankString = "7th"; break;
            case 8: rankString = "8th"; break;
            case 9: rankString = "9th"; break;
            case 10: rankString = "10th"; break;
        }

        entryTransform.Find("PositionText").GetComponent<Text>().text = rankString;
        int score = highscoreEntry.score;


        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string Name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = Name;
        transformList.Add(entryTransform);

    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Represent a single high score entry
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}


