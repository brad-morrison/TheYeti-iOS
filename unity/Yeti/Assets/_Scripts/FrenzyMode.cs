using System.Collections;
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
    private const int CountdownSeconds = 3;
    private SpriteRenderer yetiRenderer;
    private FrenzyCounter counterPrefab;

    private void Awake()
    {
        counterPrefab = frenzyCounterPrefab.GetComponent<FrenzyCounter>();
    }

    private void Start()
    {
        HideAllUI(false);
    }

    public void FrenzyCheck()
    {
        if (frenzyMode || !GM.gameManager.hikers.IsActiveHikerFrenzyTagged())
            return;

        AddFrenzyToken();
    }

    public void StartFrenzyTransition()
    {
        print("frenzy transition started");
        EnterTransitionState();
        PlayTransitionAudio();
        SetFrenzySky(true);
    }

    public void StartFrenzyMode()
    {
        Debug.Log("Frenzy mode started");
        EnterActiveState();
        StartCoroutine(FrenzyCountdown());
    }

    public void StopFrenzyMode()
    {
        Debug.Log("Frenzy mode ended");
        ExitActiveState();
        GM.audio.PlaySound(GM.audio.frenzyEnd);
        SetFrenzySky(false);
    }

    public IEnumerator FrenzyCountdown()
    {
        // wait until final 3 seconds
        yield return new WaitForSeconds(frenzyLength - CountdownSeconds);
        SpawnCountdownTimer();
        // continue timer
        yield return new WaitForSeconds(CountdownSeconds);
        StopFrenzyMode();
    }

    public void HideAllUI(bool value)
    {
        frenzyCounterPrefab.SetActive(!value);
        counterPrefab.HideCounters();
    }

    private void AddFrenzyToken()
    {
        frenzyTokenCount++;
        Instantiate(frenzyCounterPrefab);
        print("frenzy check true");
    }

    private void EnterTransitionState()
    {
        frenzyMode = true;
        frenzyUI.SetActive(true);
        GM.gameManager.LifebarState(false);
        GM.gameManager.allowInput = false;
    }

    private void EnterActiveState()
    {
        GM.gameManager.LifebarState(true);
        frenzyMode = true;
        frenzyUI.SetActive(false);
        YetiRenderer().color = Color.red;
        GM.gameManager.allowInput = true;
    }

    private void ExitActiveState()
    {
        YetiRenderer().color = Color.white;
        frenzyTokenCount = 0;
        frenzyMode = false;
    }

    private void PlayTransitionAudio()
    {
        GM.audio.PlaySound(GM.audio.frenzyStart1);
        GM.audio.PlaySound(GM.audio.frenzyStart2);
        GM.audio.PlaySound(GM.audio.goldModeStart);
    }

    private void SetFrenzySky(bool isOn)
    {
        GM.gameManager.sky.FrenzyModeSky(isOn);
    }

    private void SpawnCountdownTimer()
    {
        GameObject timer = Instantiate(GM.gameManager.timer);
        timer.GetComponent<Timer>().StartCountdown(CountdownSeconds);
    }

    private SpriteRenderer YetiRenderer()
    {
        if (yetiRenderer == null)
            yetiRenderer = GM.gameManager.yetiCharacter.GetComponent<SpriteRenderer>();

        return yetiRenderer;
    }
}
