using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HutongGames.PlayMaker;

public class Yeti : MonoBehaviour {
    
    public float yetiPunchInterval; // 0.1
    public GameObject yeti, yeti_shadow, yeti_goldOutline;
    public Sprite yeti_left, yeti_right, yeti_bothUp, yeti_bothDown1, yeti_bothDown2, yeti_dead;
    public Sprite yetiGold_left, yetiGold_right, yetiGold_bothUp;
    public Costume currentCostume;

    private void Awake() {
        int costumeId = PlayerPrefs.GetInt("costume");
        //currentCostume = game.costumes.costumesList[costumeId];
    }

    private void Start() {
        yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.both;
    }

    public void SetSprite(int state) {
        switch(state)
        {
            // drop right hand
            case 0:
                yeti.GetComponent<SpriteRenderer>().sprite = yeti_right;
                yeti_shadow.GetComponent<SpriteRenderer>().sprite = yeti_right;
                yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_right;
                break;

            // idle
            case 1:
                yeti.GetComponent<SpriteRenderer>().sprite = yeti_bothUp;
                yeti_shadow.GetComponent<SpriteRenderer>().sprite = yeti_bothUp;
                yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_bothUp;
                break;

            // drop left hand
            case 2:
                yeti.GetComponent<SpriteRenderer>().sprite = yeti_left;
                yeti_shadow.GetComponent<SpriteRenderer>().sprite = yeti_left;
                yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_left;
                break;

            // death sprite
            case 3:
                yeti.GetComponent<SpriteRenderer>().sprite = yeti_dead;
                yeti_shadow.GetComponent<SpriteRenderer>().sprite = yeti_dead;
                break;
        }

        // reset sprite back to idle
        Invoke("ResetSprite", yetiPunchInterval);
    }

    public void ResetSprite()
    {
        yeti.GetComponent<SpriteRenderer>().sprite = yeti_bothUp;
        yeti_shadow.GetComponent<SpriteRenderer>().sprite = yeti_bothUp;
        yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_bothUp;
    }
}