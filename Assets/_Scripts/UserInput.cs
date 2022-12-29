using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : GameElement
{
    Vector3 touch;

    public void ButtonPress(string function) {
        Debug.Log(function + " pressed");

        switch(function) {
            case "replay":
                Application.LoadLevel("Main");
                break;

            case "settings":
                game.mainMenu.ShowSettingsUI(true);
                game.mainMenu.ShowMainUI(false);
                break;

            case "close_settings":
                game.mainMenu.ShowSettingsUI(false);
                game.mainMenu.ShowMainUI(true);
                break;

            case "costumes_next":
                game.costumes.NextCostume();
                break;
            
            case "costumes_prev":
                game.costumes.PreviousCostume();
                break;

            case "costumes_select":
                game.costumes.SetCostume(game.costumes.currentCostume);
                Application.LoadLevel("Menu");
                break;

            case "costumes":
                Application.LoadLevel("Costumes");
                break;
                
            default:
                break;
        }
    }

    void Update()
    {
        // DEBUG CONTROLS (Keyboard)
        if (Input.GetKeyDown("a") && game.model.allowInput)
        {
            game.controller.HandleInput("left");
        }

        if (Input.GetKeyDown("d") && game.model.allowInput)
        {
            game.controller.HandleInput("right");
        }

        if (Input.GetKeyDown("g") && game.model.allowInput)
        {
            if (!game.goldMode.goldMode)
            {
                game.controller.ActivateGoldMode();
            }
            else
            {
                game.controller.DeactivateGoldMode();
            }
        }

        // flush high score
        if (Input.GetKeyDown("s") && game.model.allowInput)
        {
            game.model.highScore = 0;
            PlayerPrefs.SetInt("high_score", 0);
            PlayerPrefs.Save();
        }

        // TOUCH CONTROLS
        if (Input.GetMouseButtonDown(0) && game.model.allowInput) {
            touch = Input.mousePosition;

            // if touch is in lower half of screen
            if (touch.y < game.model.deviceScreenHeight / 2) {
                // check if touch is on the left or right of screen
                if (touch.x < game.model.deviceScreenWidth / 2) {
                    game.controller.HandleInput("left");
                } else {
                    game.controller.HandleInput("right");
                }
            }
            
        }
    }
}
