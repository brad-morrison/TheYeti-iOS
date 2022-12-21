using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GameElement
{
    private void Awake()
    {
        InstantiateHikers();
        SpawnHiker();
    }

    public void HandleTouch(string command) {

        switch(command) {
            case "left":
            game.view.SetYetiSprite(0);
            if (game.controller.UserCorrectCheck(0))
            {
                game.controller.KillHiker();
            }
            else
            {
                // game over
                GameOver();
            }
            break;

            case "right":
            game.view.SetYetiSprite(2);
            if (game.controller.UserCorrectCheck(1))
            {
                game.controller.KillHiker();
            }
            else
            {
                // game over
                GameOver();
            }
            break;

            default:
            break;
        }
    }

    public void InstantiateHikers()
    {
        SpawnHiker();
        MoveHikersUp();
        SpawnHiker();
        MoveHikersUp();
        SpawnHiker();
        MoveHikersUp();
    }

    public void SpawnHiker()
    {
        GameObject hiker;

        
        // random colour
        if (Random.Range(0,2) > 0)
        {
            // red
            hiker = Instantiate(game.model.hiker, game.model.spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            // green
            hiker = Instantiate(game.model.hiker, game.model.spawnPoint.transform.position, Quaternion.identity);
        }

        // random position
        if (Random.Range(0, 2) > 0)
        {
            hiker.GetComponent<SpriteRenderer>().flipX = true;
            hiker.GetComponent<Animator>().SetBool("Left", false);
            hiker.transform.position = new Vector2(hiker.transform.position.x + game.model.hikerOffset, hiker.transform.position.y);
        }
        else
        {
            hiker.transform.position = new Vector2(hiker.transform.position.x - game.model.hikerOffset, hiker.transform.position.y);
        }

        // if first hiker then set to active
        if (game.model.hikers.Count < 1)
        {
            game.model.activeHiker = hiker;
        }

        // add hiker to list of hikers
        game.model.hikers.Add(hiker);

    }

    public void MoveHikersUp()
    {
        foreach (GameObject hiker in game.model.hikers)
        {
            hiker.transform.position = new Vector2(hiker.transform.position.x, hiker.transform.position.y + game.model.hikerSpacing);
        }
    }

    public bool UserCorrectCheck(int pos)
    {
        if (game.model.activeHiker.transform.position.x < 0 && pos == 0)
        {
            print("correct");
            return true;
        }

        if (game.model.activeHiker.transform.position.x > 0 && pos == 1)
        {
            print("correct");
            return true;
        }

        print("incorrect");
        return false;
    }

    public void KillHiker()
    {
        GameObject target = game.model.hikers[0];
        target.GetComponent<Hiker>().StartCoroutine("Die");
        target.GetComponent<Animator>().SetBool("Dead", true);
        game.model.hikers.RemoveAt(0);
        
        game.model.activeHiker = game.model.hikers[0];
        MoveHikersUp();
        SpawnHiker();
        game.model.SetScore(game.model.score+1);
        game.model.lifebar.PunchScale();
        game.view.SetScoreUI();
    }

    public void ActivateGoldMode()
    {
        Debug.Log("gold mode on");

        // activate gold mode flag
        game.model.goldMode = true;
        // activate gold mode for lifebar
        game.model.lifebar.ActivateGoldMode();
        // activate gold mode for view (ui elements)
        game.view.ActivateGoldMode();
        GoldMultiplierRoll();
    }

    public void DeactivateGoldMode()
    {
        Debug.Log("gold mode off");

        // deactivate gold mode flag
        game.model.goldMode = false;
        // deactivate gold mode for lifebar
        game.model.lifebar.DeactivateGoldMode();
        // deactivate gold mode for view (ui elements)
        game.view.DeactivateGoldMode();
    }

    public void GoldMultiplierRoll() 
    {
        System.Random roll = new System.Random();
        game.model.goldModeMultiplier = roll.Next(1,5);
        Debug.Log("gold multiplier = " + game.model.goldModeMultiplier);
    }

    public void GameOver() {
        Debug.Log("GAME OVER");
        game.model.gameOver_UIGroup.active = true;
        game.model.allowInput = false;
    }

    

}
