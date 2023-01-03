using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrenzyMode : MonoBehaviour
{
    public GameManager manager;
    public bool frenzyMode;
    public int frenzyTokenCount;
    public GameObject frenzyCounterPrefab;
    public GameObject frenzyUI;
    // events
    public UnityEvent UI_finished = new UnityEvent();

    private void Start()
    {
        manager = transform.parent.GetComponent<GameManager>();
    }

    public void FrenzyCheck()
    {
        if (manager.hikers.hikers[0].GetComponent<Hiker>().frenzyTagged && !frenzyMode)
        {
            frenzyTokenCount++;
            Instantiate(frenzyCounterPrefab);
        }

        if (frenzyTokenCount == 3 && !manager.goldMode.goldMode)
        {
    
            StartFrenzyTransition();
        }
    }

    public void StartFrenzyTransition()
    {
        frenzyMode = true;
        frenzyUI.SetActive(true);
    }

    public void StartFrenzyMode()
    {
        Debug.Log("Frenzy mode started");
        frenzyMode = true;
        StartCoroutine(FrenzyCountdown());
        frenzyUI.SetActive(false);
        manager.yetiCharacter.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void StopFrenzyMode()
    {
        Debug.Log("Frenzy mode ended");
        manager.yetiCharacter.GetComponent<SpriteRenderer>().color = Color.white;
        frenzyTokenCount = 0;
        frenzyMode = false;
        frenzyTokenCount = 0;
    }

    public IEnumerator FrenzyCountdown()
    {
        yield return new WaitForSeconds(5);
        print("3");
        yield return new WaitForSeconds(1);
        print("2");
        yield return new WaitForSeconds(1);
        print("1");
        yield return new WaitForSeconds(1);
        StopFrenzyMode();
    }
}
