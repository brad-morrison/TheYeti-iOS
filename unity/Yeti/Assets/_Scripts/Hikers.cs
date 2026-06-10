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
        SpawnHiker();
        MoveHikersUp();
        SpawnHiker();
        MoveHikersUp();
        SpawnHiker();
        MoveHikersUp();
    }

    public void SpawnHiker()
    {
        GameObject newHiker = Instantiate(hiker, spawnPoint.transform.position, Quaternion.identity);
        Hiker hikerComponent = newHiker.GetComponent<Hiker>();
        SpriteRenderer spriteRenderer = newHiker.GetComponent<SpriteRenderer>();
        Animator animator = newHiker.GetComponent<Animator>();

        // random position
        if (Random.Range(0, 2) > 0)
        {
            // left
            hikerComponent.left = true;
            spriteRenderer.flipX = true;
            animator.SetBool("Left", false);
            newHiker.transform.position = new Vector2(newHiker.transform.position.x + hikerOffsetX, newHiker.transform.position.y);
        }
        else
        {
            // right
            hikerComponent.left = false;
            newHiker.transform.position = new Vector2(newHiker.transform.position.x - hikerOffsetX, newHiker.transform.position.y);
        }

        // if first hiker then set to active
        if (hikers.Count < 1)
        {
            activeHiker = newHiker;
        }

        // tag with frenzy roll
        int frenzyRoll = Random.Range(1, GM.gameManager.gameplayVariables.frenzyHikerChance);
        if (frenzyRoll == 1 && !GM.gameManager.goldMode.goldMode && !GM.gameManager.frenzyMode.frenzyMode && GM.gameManager.allowFrenzyMode)
        {
            hikerComponent.frenzyTagged = true;
            spriteRenderer.color = Color.red;
        }

        // add hiker to list of hikers
        hikers.Add(newHiker);

        // add as child of hikers object
        newHiker.transform.parent = this.gameObject.transform;
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
        GameObject target = hikers[0];
        Hiker targetHiker = target.GetComponent<Hiker>();
        SpriteRenderer targetRenderer = target.GetComponent<SpriteRenderer>();
        Animator targetAnimator = target.GetComponent<Animator>();

        targetRenderer.sortingOrder = 10;
        targetHiker.StartCoroutine(targetHiker.Die());
        targetAnimator.SetBool("Dead", true);
        hikers.RemoveAt(0);
        
        activeHiker = hikers[0];
        MoveHikersUp();
        SpawnHiker();
        
    }

    public void DisableAnimations() {
        foreach (GameObject hiker in hikers) {
            hiker.GetComponent<Hiker>().DisableAnimations();
        }
    }



    
}
