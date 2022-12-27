using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using HutongGames.PlayMaker;

public class GameView : GameElement
{Sequence OpenGameOverUI_seq;
    void Start() {
        
    }

    public void SetYetiSprite(int state)
    {
        switch(state)
        {
            // drop right hand
            case 0:
                game.model.yeti.GetComponent<SpriteRenderer>().sprite = game.model.yeti_right;
                game.model.yeti_shadow.GetComponent<SpriteRenderer>().sprite = game.model.yeti_right;
                game.model.yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = game.model.yetiGold_right;
                break;

            // idle
            case 1:
                game.model.yeti.GetComponent<SpriteRenderer>().sprite = game.model.yeti_bothUp;
                game.model.yeti_shadow.GetComponent<SpriteRenderer>().sprite = game.model.yeti_bothUp;
                game.model.yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = game.model.yetiGold_bothUp;
                break;

            // drop left hand
            case 2:
                game.model.yeti.GetComponent<SpriteRenderer>().sprite = game.model.yeti_left;
                game.model.yeti_shadow.GetComponent<SpriteRenderer>().sprite = game.model.yeti_left;
                game.model.yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = game.model.yetiGold_left;
                break;

            // death sprite
            case 3:
                game.model.yeti.GetComponent<SpriteRenderer>().sprite = game.model.yeti_dead;
                game.model.yeti_shadow.GetComponent<SpriteRenderer>().sprite = game.model.yeti_dead;
                break;
        }

        // reset sprite back to idle
        Invoke("ResetYetiSprite", game.model.yetiPunchInterval);
    }

    public void ActivateGoldMode()
    {
        foreach(Transform flame in game.model.goldFlames.transform)
        {
            flame.GetComponent<SpriteRenderer>().enabled = true;
        }
        game.model.goldYeti.SetActive(true);

        // Run fsm
        game.model.FSM_GoldModeAnimations.SendEvent("start");
    }

    public void DeactivateGoldMode()
    {
        foreach (Transform flame in game.model.goldFlames.transform)
        {
            flame.GetComponent<SpriteRenderer>().enabled = false;
        }
        game.model.goldYeti.SetActive(false);
    }

    public void ResetYetiSprite()
    {
        game.model.yeti.GetComponent<SpriteRenderer>().sprite = game.model.yeti_bothUp;
        game.model.yeti_shadow.GetComponent<SpriteRenderer>().sprite = game.model.yeti_bothUp;
        game.model.yeti_goldOutline.GetComponent<SpriteRenderer>().sprite = game.model.yetiGold_bothUp;
    }

    // ui
    public void SetScoreUI()
    {
        game.model.text_score.GetComponent<TextMeshProUGUI>().text = game.model.score.ToString();
    }

    
    public void GameOverView() {

        DisableAllAnimations();

        // set game over ui scores
        // if 0 then use the letter 'o' instead, 0 looks like an 8 with chosen font
        game.model.finalScore.GetComponent<TextMeshPro>().text = game.model.score == 0 ? "o" : game.model.score.ToString();
        game.model.finalBest.GetComponent<TextMeshPro>().text = game.model.highScore == 0 ? "o" : game.model.highScore.ToString();

        // Run fsm
        game.model.FSM_GameOverAnimations.SendEvent("start");
    
    }

     public void DisableAllAnimations() {

        // stop bar from moving
        game.model.lifebar.animate = false;
        game.model.lifeBar_ScrollSpeed = 0;

        // disable current hikers animations
        foreach (GameObject hiker in game.model.hikers)
        {
            hiker.GetComponent<Hiker>().DisableAnimations();
        }

     }

    public void ChangeSprite(GameObject obj, Sprite sprite) {
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public IEnumerator ChangeSpriteDelay(GameObject obj, Sprite sprite, float delay) {
        yield return new WaitForSeconds(2);
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    } 
}
