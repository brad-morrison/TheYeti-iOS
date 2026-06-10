using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Controller for Frenzy Mode
//

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

    private void Start()
    {
        HideAllUI(false);
    }

    public void FrenzyCheck()
    {
        if (GM.gameManager.hikers.hikers[0].GetComponent<Hiker>().frenzyTagged && !frenzyMode)
        {
            frenzyTokenCount++;
            Instantiate(frenzyCounterPrefab);
            print("frenzy check true");
        }
    }

    public void StartFrenzyTransition()
    {
        print("frenzy transition started");
        frenzyMode = true;
        frenzyUI.SetActive(true);
        GM.gameManager.LifebarState(false); // pause lifebar
        GM.gameManager.allowInput = false;
        GM.audio.PlaySound(GM.audio.frenzyStart1);
        GM.audio.PlaySound(GM.audio.frenzyStart2);
        GM.audio.PlaySound(GM.audio.goldModeStart);
        GM.gameManager.sky.FrenzyModeSky(true);
    }

    public void StartFrenzyMode()
    {
        Debug.Log("Frenzy mode started");
        GM.gameManager.LifebarState(true); // restart lifebar
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
        GM.gameManager.sky.FrenzyModeSky(false);
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

    public void HideAllUI(bool value)
    {
        frenzyCounterPrefab.SetActive(!value);
        frenzyCounterPrefab.GetComponent<FrenzyCounter>().HideCounters();
    }
}
