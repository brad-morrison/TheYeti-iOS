using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameView : GameElement
{
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

    public void OpenGameOverUI() {

        game.model.gameOver_UIGroup.active = true;
    }
}
