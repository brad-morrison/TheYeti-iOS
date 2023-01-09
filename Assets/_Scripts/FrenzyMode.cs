using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrenzyMode : MonoBehaviour
{
    public MasterManager master;
    public GameManager manager;
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
        master = GameObject.Find("MASTER_MANAGER").GetComponent<MasterManager>();
        manager = transform.parent.GetComponent<GameManager>();
    }

    public void FrenzyCheck()
    {
        if (manager.hikers.hikers[0].GetComponent<Hiker>().frenzyTagged && !frenzyMode)
        {
            frenzyTokenCount++;
            Instantiate(frenzyCounterPrefab);
        }
    }

    public void StartFrenzyTransition()
    {
        frenzyMode = true;
        frenzyUI.SetActive(true);
        manager.allowInput = false;
        master.audio.PlaySound(master.audio.frenzyStart1);
        master.audio.PlaySound(master.audio.frenzyStart2);
        master.audio.PlaySound(master.audio.goldModeStart);
    }

    public void StartFrenzyMode()
    {
        Debug.Log("Frenzy mode started");
        frenzyMode = true;
        StartCoroutine(FrenzyCountdown());
        frenzyUI.SetActive(false);
        manager.yetiCharacter.GetComponent<SpriteRenderer>().color = Color.red;
        manager.allowInput = true;
    }

    public void StopFrenzyMode()
    {
        Debug.Log("Frenzy mode ended");
        manager.yetiCharacter.GetComponent<SpriteRenderer>().color = Color.white;
        frenzyTokenCount = 0;
        frenzyMode = false;
        frenzyTokenCount = 0;
        master.audio.PlaySound(master.audio.frenzyEnd);
    }

    public IEnumerator FrenzyCountdown()
    {
        // wait until final 3 seconds
        yield return new WaitForSeconds(frenzyLength-3);
        // create timer ui object and start
        GameObject timer = Instantiate(manager.timer);
        timer.GetComponent<Timer>().StartCountdown(3);
        // continue timer
        yield return new WaitForSeconds(3);
        StopFrenzyMode();
    }
}
