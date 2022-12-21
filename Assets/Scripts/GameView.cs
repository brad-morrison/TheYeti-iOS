using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameView : GameElement
{
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
        
        game.model.gameOverUI_Group.active = true;
        Sequence OpenGameOverUI_seq = DOTween.Sequence();
        
        // move in top and bottom UI
        OpenGameOverUI_seq
            .Append(game.model.gameOverUI_top.transform.DOMoveY(1.4f, 1f).From().SetEase(Ease.OutBounce))
            .Insert(0.2f, game.model.gameOverUI_buttons.transform.DOMove(new Vector3(0,-10,0), 1).From().SetEase(Ease.OutQuint))
        // move in yeti and hiker
            .Insert(0.4f, game.model.gameOverUI_yeti.transform.DOMoveY(-5f, 1).From().SetEase(Ease.OutQuint))
            .Insert(0.6f, game.model.gameOverUI_hiker.transform.DOMoveY(1.8f, 1).From().SetEase(Ease.OutBounce))
        // wait and change sprites
            .InsertCallback(2f, ()=>ChangeSprite(game.model.gameOverUI_hiker, game.model.hiker_red_smiling));

    }

    public void ChangeSprite(GameObject obj, Sprite sprite) {
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
