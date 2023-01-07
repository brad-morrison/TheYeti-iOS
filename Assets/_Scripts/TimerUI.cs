using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public GameObject top, bottom;
    public GameObject banner;

    public void PlayCycle(int number)
    {
        if (number == 0)
        {
            top.SetActive(false);
            return;
        }

        //gameObject.SetActive(true);
        //topText.SetText(number.ToString());
        //bottomText.SetText(number.ToString());
    }

    public void Hide()
    {
        top.SetActive(false);
    }
}
