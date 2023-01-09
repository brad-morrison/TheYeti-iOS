using UnityEngine;
using System.Collections;
using DG.Tweening.Core.Easing;

public class GameControls : MonoBehaviour
{
    public GameManager manager;
    Vector3 touch;

	// Update is called once per frame
	void Update()
	{
        // DEBUG CONTROLS (Keyboard)
        if (Input.GetKeyDown("a") && manager.allowInput)
        {
            manager.HandleInput("left");
        }

        if (Input.GetKeyDown("d") && manager.allowInput)
        {
            manager.HandleInput("right");
        }

        if (Input.GetKeyDown("b") && manager.allowInput)
        {
            manager.FallingBones(true);
        }

        if (Input.GetKeyDown("n") && manager.allowInput)
        {
            manager.FallingBones(false);
        }

        if (Input.GetKeyDown("g") && manager.allowInput)
        {
            if (!manager.goldMode.goldMode)
            {
                manager.ActivateGoldMode();
            }
            else
            {
                manager.DeactivateGoldMode();
            }
        }

        // flush high score
        if (Input.GetKeyDown("s") && manager.allowInput)
        {
            Debug.Log("reset scores");
            PlayerPrefs.SetInt("high_score", 0);
            PlayerPrefs.SetInt("kills", 0);
            PlayerPrefs.Save();
            manager.highScore = 0;
            manager.totalKills_counter = 0;
        }

        // for debug - add 100 to high score
        if (Input.GetKeyDown("h") && manager.allowInput)
        {
            Debug.Log("added 100 to high score | now - " + manager.highScore);
            int data = PlayerPrefs.GetInt("high_score", 0);
            manager.highScore = manager.highScore + 100;
            PlayerPrefs.SetInt("high_score", data + 100);
            PlayerPrefs.Save();
        }

        // for debug - add 100 to kills
        if (Input.GetKeyDown("k") && manager.allowInput)
        {
            Debug.Log("added 100 to kills | now - " + manager.totalKills_counter);
            int data = PlayerPrefs.GetInt("total_kills", 0);
            manager.totalKills = manager.totalKills_counter + 100;
            PlayerPrefs.SetInt("high_score", data + 100);
            PlayerPrefs.Save();
        }

        // TOUCH CONTROLS

        if (Input.GetMouseButtonUp(0) && manager.allowInput)
        {
            touch = Input.mousePosition;

            // if touch is in lower half of screen
            if (touch.y < manager.deviceScreenHeight / 2)
            {
                // check if touch is on the left or right of screen
                if (touch.x < manager.deviceScreenWidth / 2)
                {
                    
                    manager.HandleInput("left");
                }
                else
                {
                    
                    manager.HandleInput("right");
                }
            }

        }
    }
}

