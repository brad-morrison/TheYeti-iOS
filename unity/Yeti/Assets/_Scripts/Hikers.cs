using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Hikers : TheYeti {
    // gameplay variables
    public float hikerOffsetX; // 0.22
    public float hikerOffsetY; // 0.3
    // variables
    public Sprite hikerRed_idle, hikerRed_lookingDown, hikerRed_axeUp, hikerRed_smiling;
    public List<GameObject> hikers = new List<GameObject>();
    public GameObject hiker_standing_left, hiker_standing_right;
    // prefabs
    public GameObject hiker;
    // markers
    public GameObject spawnPoint;
    public GameObject activeHiker;
    // events
    public UnityEvent hikerShake = new UnityEvent();
    private DOTweenAnimation shakeAnimation;

    private void Awake()
    {
        shakeAnimation = GetComponent<DOTweenAnimation>();
    }

    public void InitHikers() {
        for (int i = 0; i < 3; i++)
        {
            SpawnHiker();
            MoveHikersUp();
        }
    }

    public bool IsActiveHikerOnLeft()
    {
        return ActiveHiker().left;
    }

    public bool IsActiveHikerFrenzyTagged()
    {
        return ActiveHiker().frenzyTagged;
    }

    private Hiker ActiveHiker()
    {
        return activeHiker.GetComponent<Hiker>();
    }

    public void SpawnHiker()
    {
        GameObject newHiker = Instantiate(hiker, spawnPoint.transform.position, Quaternion.identity);
        Hiker hikerComponent = newHiker.GetComponent<Hiker>();
        HikerSide side = Random.Range(0, 2) > 0 ? HikerSide.Left : HikerSide.Right;
        hikerComponent.SetSide(side, hikerOffsetX);

        // tag with frenzy roll
        int frenzyRoll = Random.Range(1, GM.gameManager.gameplayVariables.frenzyHikerChance);
        if (frenzyRoll == 1 && GM.gameManager.CanTagFrenzyHiker)
        {
            hikerComponent.SetFrenzyTagged(true);
        }

        // add as child of hikers object
        newHiker.transform.parent = transform;
        EnqueueHiker(newHiker);
    }

    private void EnqueueHiker(GameObject newHiker)
    {
        hikers.Add(newHiker);
        UpdateActiveHiker();
    }

    public void MoveHikersUp()
    {
        foreach (GameObject hiker in hikers)
        {
            hiker.transform.position = new Vector2(hiker.transform.position.x, hiker.transform.position.y + hikerOffsetY);
        }

        ShakeHikers();
    }

    public void ShakeHikers()
    {
        shakeAnimation.DORestart();
    }

    public void KillHiker()
    {
        GameObject target = DequeueActiveHiker();
        Hiker targetHiker = target.GetComponent<Hiker>();

        targetHiker.PlayDeath();
        MoveHikersUp();
        SpawnHiker();
        
    }

    private GameObject DequeueActiveHiker()
    {
        GameObject target = hikers[0];
        hikers.RemoveAt(0);
        UpdateActiveHiker();
        return target;
    }

    private void UpdateActiveHiker()
    {
        activeHiker = hikers.Count > 0 ? hikers[0] : null;
    }

    public void DisableAnimations() {
        foreach (GameObject hiker in hikers) {
            hiker.GetComponent<Hiker>().DisableAnimations();
        }
    }



    
}
