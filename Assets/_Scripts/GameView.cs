using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using HutongGames.PlayMaker;

public class GameView : GameElement
{
    public void ActivateGoldMode()
    {
        foreach(Transform flame in game.goldMode.goldFlames.transform)
        {
            flame.GetComponent<SpriteRenderer>().enabled = true;
        }
        game.yeti.yeti_goldOutline.SetActive(true);

        // Run fsm
        game.model.FSM_GoldModeAnimations.SendEvent("start");
    }

    public void DeactivateGoldMode()
    {
        foreach (Transform flame in game.goldMode.goldFlames.transform)
        {
            flame.GetComponent<SpriteRenderer>().enabled = false;
        }
        game.yeti.yeti_goldOutline.SetActive(false);
    }
    
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
        foreach (GameObject hiker in game.model.hikers.hikers)
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
