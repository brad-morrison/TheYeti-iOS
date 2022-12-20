using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : GameElement
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            game.view.SetYetiSprite(0);
            if (game.controller.UserCorrectCheck(0))
            {
                game.controller.KillHiker();
            }
        }

        if (Input.GetKeyDown("d"))
        {
            game.view.SetYetiSprite(2);
            if (game.controller.UserCorrectCheck(1))
            {
                game.controller.KillHiker();
            }
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
    }
}
