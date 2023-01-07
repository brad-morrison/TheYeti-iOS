using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public Audio audio;
    public GameObject top, outline;
    public UnityEvent tick;
    public int _time;

    private void Awake()
    {
        audio = GameObject.Find("Audio").GetComponent<Audio>();
    }

    public void StartCountdown(int time)
    {
        gameObject.SetActive(true);
        StartCoroutine(_StartCountdown(time));
    }

    public void Finish()
    {
        Destroy(gameObject);
    }

    public IEnumerator _StartCountdown(int time)
    {
        // countdown and change text
        for (int i = time; i > 0; i--)
        {
            // set text
            top.GetComponent<TextMeshPro>().text = i.ToString();
            outline.GetComponent<TextMeshPro>().text = i.ToString();
            // play animation
            GetComponent<DOTweenAnimation>().DOPlayById("tick");
            GetComponent<DOTweenAnimation>().DORestartById("tick");
            // play audio
            audio.PlaySound(audio.timerTick);
            // wait
            yield return new WaitForSeconds(1);
            
        }

        // at 0
        top.GetComponent<TextMeshPro>().text = "o";
        outline.GetComponent<TextMeshPro>().text = "o";
        GetComponent<DOTweenAnimation>().DOPlayById("end");
    }

}
