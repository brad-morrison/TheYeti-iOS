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
        if (Input.GetKeyDown("a"))
        {
            game.controller.HandleTouch("left");
        }

        if (Input.GetKeyDown("d"))
        {
            game.controller.HandleTouch("right");
        }

        if (Input.GetKeyDown("g"))
        {
            if (!game.model.goldMode)
            {
                game.controller.ActivateGoldMode();
            }
            else
            {
                game.controller.DeactivateGoldMode();
            }
        }

        // TOUCH CONTROLS
        if (Input.GetMouseButtonDown(0)) {
            touch = Input.mousePosition;

            if (touch.x < game.model.deviceScreenWidth / 2) {
                game.controller.HandleTouch("left");
            }
            else {
                game.controller.HandleTouch("right");
            }
        }
    }
}
