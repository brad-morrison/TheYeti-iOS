using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : TheYeti
{
    // listen to events
    private void OnEnable()
    {
        Actions.onButtonPressed += ButtonPress;
    }

    private void OnDisable()
    {
        Actions.onButtonPressed -= ButtonPress;
    }

    public void ButtonPress(string function) {

        switch(function) {
            case "replay":
                Application.LoadLevel("Main");
                break;

            case "settings":
                GM.mainMenu.ShowSettingsUI(true);
                GM.mainMenu.ShowMainUI(false);
                break;

            case "close_settings":
                GM.mainMenu.ShowSettingsUI(false);
                GM.mainMenu.ShowMainUI(true);
                break;

            case "music":
                GM.mainMenu.Music(false);
                GM.audio.Music(false);
                break;

            case "music_mute":
                GM.mainMenu.Music(true);
                GM.audio.Music(true);
                break;

            case "sfx":
                GM.mainMenu.Sfx(false);
                GM.audio.sfxOn = false;
                PlayerPrefs.SetInt("sfx", 0);
                break;

            case "sfx_mute":
                GM.mainMenu.Sfx(true);
                GM.audio.sfxOn = true;
                PlayerPrefs.SetInt("sfx", 1);
                break;

            case "costumes_next":
                GM.costumeManager.NextCostume();
                break;
            
            case "costumes_prev":
                GM.costumeManager.PreviousCostume();
                break;

            case "costumes_select":
                GM.costumeManager.SetCostume();
                Application.LoadLevel("Menu");
                break;

            case "costumes":
                Application.LoadLevel("Costumes");
                break;

            case "Menu":
                Application.LoadLevel("Menu");
                break;

            case "remove_ads":
                break;

            case "restore":
                break;

            case "leaderboard":
                GM.leaderboards.ShowLeaderboards();
                break;

            default:
                break;
        }
    }
    
}
