using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    Vector3 touch;
    public GameObject mainMenu, costumes;
    public GameManager manager;

    public void ButtonPress(string function) {

        switch(function) {
            case "replay":
                Application.LoadLevel("Main");
                break;

            case "settings":
                mainMenu.GetComponent<MainMenu>().ShowSettingsUI(true);
                mainMenu.GetComponent<MainMenu>().ShowMainUI(false);
                break;

            case "close_settings":
                mainMenu.GetComponent<MainMenu>().ShowSettingsUI(false);
                mainMenu.GetComponent<MainMenu>().ShowMainUI(true);
                break;

            case "music":
                mainMenu.GetComponent<MainMenu>().Music(false);
                break;

            case "music_mute":
                mainMenu.GetComponent<MainMenu>().Music(true);
                break;

            case "sfx":
                mainMenu.GetComponent<MainMenu>().Sfx(false);
                break;

            case "sfx_mute":
                mainMenu.GetComponent<MainMenu>().Sfx(true);
                break;

            case "costumes_next":
                costumes.GetComponent<CostumeManager>().NextCostume();
                break;
            
            case "costumes_prev":
                costumes.GetComponent<CostumeManager>().PreviousCostume();
                break;

            case "costumes_select":
                //costumes.GetComponent<Costumes>().SetCostume(costumes.GetComponent<Costumes>().currentCostume);
                costumes.GetComponent<CostumeManager>().SetCostume();
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
        if (Input.GetKeyDown("a") && manager.allowInput)
        {
            manager.HandleInput("left");
        }

        if (Input.GetKeyDown("d") && manager.allowInput)
        {
            manager.HandleInput("right");
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
            PlayerPrefs.SetInt("total_kills", 0);
            PlayerPrefs.Save();
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
        if (Input.GetMouseButtonDown(0) && manager.allowInput) {
            touch = Input.mousePosition;

            // if touch is in lower half of screen
            if (touch.y < manager.deviceScreenHeight / 2) {
                // check if touch is on the left or right of screen
                if (touch.x < manager.deviceScreenWidth / 2) {
                    manager.HandleInput("left");
                } else {
                    manager.HandleInput("right");
                }
            }
            
        }
    }
}
