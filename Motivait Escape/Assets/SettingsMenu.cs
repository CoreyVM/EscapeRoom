using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public void SetFullscreen(bool value)
    {
  
    }

    public void SetQuality(int qualityIndex) //Use the index to set the game quality
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
  

}
