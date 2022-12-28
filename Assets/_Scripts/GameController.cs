using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HutongGames.PlayMaker;

public class GameController : GameElement
{
    private void Start()
    {
        
        game.model.score = 0;
        game.hikers.InitHikers();
        game.hikers.SpawnHiker();
    }

    public void HandleInput(string command) {

        switch(command) {
            case "left":
            game.yeti.SetSprite(0);
            if (game.controller.IsPlayerCorrect(0))
            {
                game.model.SetScore(AddToScore());
                game.model.lifebar.PunchScale();
                game.view.SetScoreUI();
                game.hikers.KillHiker();
            }
            else
            {
                game.gameOver.SetGameOver();
            }
            break;

            case "right":
            game.yeti.SetSprite(2);
            if (game.controller.IsPlayerCorrect(1))
            {
                game.model.SetScore(AddToScore());
                game.model.lifebar.PunchScale();
                game.view.SetScoreUI();
                game.hikers.KillHiker();
            }
            else
            {
                game.gameOver.SetGameOver();
            }
            break;

            default:
            break;
        }
    }

    public int AddToScore() {
        if (game.goldMode.goldMode) {
            return game.goldMode.goldModeMultiplier + 1;
        } else {
            return 1;
        }
    }

    public bool IsPlayerCorrect(int pos)
    {
        if (!game.hikers.activeHiker.GetComponent<Hiker>().left && pos == 0)
        {
            return true;
        }

        if (game.hikers.activeHiker.GetComponent<Hiker>().left && pos == 1)
        {
            return true;
        }

        return false;
    }

    public void ActivateGoldMode()
    {
        game.goldMode.GoldModeAnnounce();
    }

    public void DeactivateGoldMode()
    {
        game.goldMode.DeactivateGoldMode();
    }

    public void DisableAllAnimations() {

        // disable lifebar animations
        game.model.lifebar.animate = false;
        game.model.lifeBar_ScrollSpeed = 0;

        // disable hiker animations
        game.hikers.DisableAnimations();

     }

}
