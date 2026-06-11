using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

// Controls UI for countdown timer
//

public class Timer : TheYeti
{
    public GameObject top, outline;
    public UnityEvent tick;
    public int _time;
    private TextMeshPro topText;
    private TextMeshPro outlineText;
    private DOTweenAnimation tweenAnimation;

    private void Awake()
    {
        topText = top.GetComponent<TextMeshPro>();
        outlineText = outline.GetComponent<TextMeshPro>();
        tweenAnimation = GetComponent<DOTweenAnimation>();
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
            SetText(i.ToString());
            // play animation
            tweenAnimation.DOPlayById("tick");
            tweenAnimation.DORestartById("tick");
            // play audio
            GM.audio.PlaySound(GM.audio.timerTick);
            // wait
            yield return new WaitForSeconds(1);
            
        }

        // at 0
        SetText("o");
        tweenAnimation.DOPlayById("end");
    }

    private void SetText(string text)
    {
        topText.text = text;
        outlineText.text = text;
    }
}
