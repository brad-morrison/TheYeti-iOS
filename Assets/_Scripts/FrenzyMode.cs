using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrenzyMode : TheYeti
{
    public bool frenzyMode;
    public int frenzyTokenCount;
    public int frenzyLength;
    public GameObject frenzyCounterPrefab;
    public GameObject frenzyUI;
    // events
    public UnityEvent UI_finished = new UnityEvent();
    public UnityEvent<int> countdownTick;

    public void FrenzyCheck()
    {
        if (GM.gameManager.hikers.hikers[0].GetComponent<Hiker>().frenzyTagged && !frenzyMode)
        {
            frenzyTokenCount++;
            Instantiate(frenzyCounterPrefab);
        }
    }

    public void StartFrenzyTransition()
    {
        frenzyMode = true;
        frenzyUI.SetActive(true);
        GM.gameManager.allowInput = false;
        GM.audio.PlaySound(GM.audio.frenzyStart1);
        GM.audio.PlaySound(GM.audio.frenzyStart2);
        GM.audio.PlaySound(GM.audio.goldModeStart);
    }

    public void StartFrenzyMode()
    {
        Debug.Log("Frenzy mode started");
        frenzyMode = true;
        StartCoroutine(FrenzyCountdown());
        frenzyUI.SetActive(false);
        GM.gameManager.yetiCharacter.GetComponent<SpriteRenderer>().color = Color.red;
        GM.gameManager.allowInput = true;
    }

    public void StopFrenzyMode()
    {
        Debug.Log("Frenzy mode ended");
        GM.gameManager.yetiCharacter.GetComponent<SpriteRenderer>().color = Color.white;
        frenzyTokenCount = 0;
        frenzyMode = false;
        frenzyTokenCount = 0;
        GM.audio.PlaySound(GM.audio.frenzyEnd);
    }

    public IEnumerator FrenzyCountdown()
    {
        // wait until final 3 seconds
        yield return new WaitForSeconds(frenzyLength-3);
        // create timer ui object and start
        GameObject timer = Instantiate(GM.gameManager.timer);
        timer.GetComponent<Timer>().StartCountdown(3);
        // continue timer
        yield return new WaitForSeconds(3);
        StopFrenzyMode();
    }
}
