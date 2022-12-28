using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hikers : GameElement {
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
        GameObject newHiker;

        // random colour
        if (Random.Range(0,2) > 0)
        {
            // red
            newHiker = Instantiate(hiker, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            // green
            newHiker = Instantiate(hiker, spawnPoint.transform.position, Quaternion.identity);
        }

        // random position
        if (Random.Range(0, 2) > 0)
        {
            // left
            newHiker.GetComponent<Hiker>().left = true;
            newHiker.GetComponent<SpriteRenderer>().flipX = true;
            newHiker.GetComponent<Animator>().SetBool("Left", false);
            newHiker.transform.position = new Vector2(newHiker.transform.position.x + hikerOffsetX, newHiker.transform.position.y);
        }
        else
        {
            // right
            newHiker.GetComponent<Hiker>().left = false;
            newHiker.transform.position = new Vector2(newHiker.transform.position.x - hikerOffsetX, newHiker.transform.position.y);
        }

        // if first hiker then set to active
        if (hikers.Count < 1)
        {
            activeHiker = newHiker;
        }

        // add hiker to list of hikers
        hikers.Add(newHiker);
    }

    public void MoveHikersUp()
    {
        foreach (GameObject hiker in hikers)
        {
            hiker.transform.position = new Vector2(hiker.transform.position.x, hiker.transform.position.y + hikerOffsetY);
        }
    }

    public void KillHiker()
    {
        GameObject target = hikers[0];
        target.GetComponent<Hiker>().StartCoroutine("Die");
        target.GetComponent<Animator>().SetBool("Dead", true);
        hikers.RemoveAt(0);
        
        activeHiker = hikers[0];
        MoveHikersUp();
        SpawnHiker();
        
    }



    
}