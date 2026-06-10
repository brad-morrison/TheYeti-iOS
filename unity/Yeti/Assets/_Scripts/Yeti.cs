using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HutongGames.PlayMaker;

public class Yeti : TheYeti {
    
    public float yetiPunchInterval; // 0.1
    public GameObject yeti, yeti_goldOutline;
    public Sprite yetiGold_left, yetiGold_right, yetiGold_bothUp;
    public Costume currentCostume;

    private void Start() {

        currentCostume = GM.gameManager.costumesList[PlayerPrefs.GetInt("costume")];
        yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.both;
        GM.gameManager.yetiCharacter_gameOver.GetComponent<SpriteRenderer>().sprite = currentCostume.both;
    }

    public void SetSprite(string sprite) {
        switch(sprite)
        {
            case "left":
                yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.left;
                yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_right;
                break;

            case "both":
                yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.both;
                yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_bothUp;
                break;

            case "right":
                yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.right;
                yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_left;
                break;

            case "dead":
                yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.dead;
                break;

            case "idle1":
                yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.idle1;
                break;

            case "idle2":
                yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.idle2;
                break;
        }

        // reset sprite back to idle
        Invoke("ResetSprite", yetiPunchInterval);
    }

    public void ResetSprite()
    {
        yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.both;
        yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = yetiGold_bothUp;
    }
}