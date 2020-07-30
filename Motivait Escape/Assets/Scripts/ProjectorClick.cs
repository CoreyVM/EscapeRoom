using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectorClick : MonoBehaviour
{
    public RawImage Slide;
    public List<Texture2D> Textures;
    private int CurrentTexIndex = 0;

    private bool isOn = false;

    public void ToggleSlide()
    {
        if (!isOn)
        {
            isOn = true;
            Slide.enabled = true;
            StartCoroutine(ChangeImage());
        }
        else
        {
            isOn = false;
            Slide.enabled = false;
            StopAllCoroutines();
        }
    }
    IEnumerator ChangeImage()
    {
        Slide.texture = Textures[CurrentTexIndex];
        yield return new WaitForSeconds(5f);
        CurrentTexIndex++;
        if (CurrentTexIndex >= Textures.Count)
            CurrentTexIndex = 0;
        StartCoroutine(ChangeImage());
    }
}


