using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLight : MonoBehaviour
{
    public GameObject lightsource;
    private bool on = false;

  public  void ToggleLight()
    {
        if (!on)
        {
            on = true;
            lightsource.SetActive(true);
        }

        else
        {           
                on = false;
                lightsource.SetActive(false);        
        }
    }
}
