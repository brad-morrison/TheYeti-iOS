using UnityEngine;
using System.Collections;
using DG.Tweening.Core.Easing;

public class GameControls : TheYeti
{
    Vector3 touch;

	// Update is called once per frame
	void Update()
	{
        // DEBUG CONTROLS (Keyboard)
        if (Input.GetKeyDown("a") && GM.gameManager.allowInput)
        {
            GM.gameManager.HandleInput("left");
        }

        if (Input.GetKeyDown("d") && GM.gameManager.allowInput)
        {
            GM.gameManager.HandleInput("right");
        }

        if (Input.GetKeyDown("b") && GM.gameManager.allowInput)
        {
            GM.gameManager.FallingBones(true);
        }

        if (Input.GetKeyDown("n") && GM.gameManager.allowInput)
        {
            GM.gameManager.FallingBones(false);
        }

        if (Input.GetKeyDown("g") && GM.gameManager.allowInput)
        {
            if (!GM.gameManager.goldMode.goldMode)
            {
                GM.gameManager.ActivateGoldMode();
            }
            else
            {
                GM.gameManager.DeactivateGoldMode();
            }
        }

        // flush high score
        if (Input.GetKeyDown("s") && GM.gameManager.allowInput)
        {
            Debug.Log("reset scores");
            PlayerPrefs.SetInt("high_score", 0);
            PlayerPrefs.SetInt("kills", 0);
            PlayerPrefs.Save();
            GM.gameManager.highScore = 0;
            GM.gameManager.totalKills_counter = 0;
        }

        // for debug - add 100 to high score
        if (Input.GetKeyDown("h") && GM.gameManager.allowInput)
        {
            Debug.Log("added 100 to high score | now - " + GM.gameManager.highScore);
            int data = PlayerPrefs.GetInt("high_score", 0);
            GM.gameManager.highScore = GM.gameManager.highScore + 100;
            PlayerPrefs.SetInt("high_score", data + 100);
            PlayerPrefs.Save();
        }

        // for debug - add 100 to kills
        if (Input.GetKeyDown("k") && GM.gameManager.allowInput)
        {
            Debug.Log("added 100 to kills | now - " + GM.gameManager.totalKills_counter);
            int data = PlayerPrefs.GetInt("total_kills", 0);
            GM.gameManager.totalKills = GM.gameManager.totalKills_counter + 100;
            PlayerPrefs.SetInt("high_score", data + 100);
            PlayerPrefs.Save();
        }

        // TOUCH CONTROLS

        if (Input.GetMouseButtonUp(0) && GM.gameManager.allowInput)
        {
            touch = Input.mousePosition;

            // if touch is in lower half of screen
            if (touch.y < GM.gameManager.deviceScreenHeight / 2)
            {
                // check if touch is on the left or right of screen
                if (touch.x < GM.gameManager.deviceScreenWidth / 2)
                {

                    GM.gameManager.HandleInput("left");
                }
                else
                {

                    GM.gameManager.HandleInput("right");
                }
            }

        }
    }
}

