using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : GameElement
{
    Vector3 touch;
    // Update is called once per frame
    void Update()
    {
        // DEBUG CONTROLS (Keyboard)
        if (Input.GetKeyDown("a") && game.model.allowInput)
        {
            game.controller.HandleTouch("left");
        }

        if (Input.GetKeyDown("d") && game.model.allowInput)
        {
            game.controller.HandleTouch("right");
        }

        if (Input.GetKeyDown("g") && game.model.allowInput)
        {
            if (!game.model.goldMode)
            {
                game.controller.GoldMode_Transition();
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
                    game.controller.HandleTouch("left");
                } else {
                    game.controller.HandleTouch("right");
                }
            }
            
        }
    }
}
