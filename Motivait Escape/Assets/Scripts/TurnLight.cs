using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLight : MonoBehaviour
{
    public GameObject lightsource;
    private bool isOn = false;

  public  void ToggleLight()
    {
        isOn = !isOn;
        lightsource.SetActive(isOn);
    }
}
