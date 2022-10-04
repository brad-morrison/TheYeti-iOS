using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomColorChange : MonoBehaviour
{
    public GameObject obj;
    public TextMeshPro text;
    public Color[] colors;
    public float time;
    int counter;

    // Start is called before the first frame update
    void Start()
    {
        FadeTo(colors[0]);
    }

    public void LoadNextColor()
    {
        counter++;
        if (counter != colors.Length)
            FadeTo(colors[counter]);
        else
            FadeTo(colors[0]);
    }

    public void FadeTo(Color target)
    {
        iTween.ColorTo(obj, iTween.Hash("time", time, "oncomplete", "LoadNextColor", "easetype", iTween.EaseType.linear, "color", target));
    }
}
