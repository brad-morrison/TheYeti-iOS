using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : MonoBehaviour
{
    public MasterManager master;

    private void Start()
    {
        // get master
        master = gameObject.transform.parent.GetComponent<MasterManager>();
    }

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
                master.mainMenu.ShowSettingsUI(true);
                master.mainMenu.ShowMainUI(false);
                break;

            case "close_settings":
                master.mainMenu.ShowSettingsUI(false);
                master.mainMenu.ShowMainUI(true);
                break;

            case "music":
                master.mainMenu.Music(false);
                master.audio.Music(false);
                break;

            case "music_mute":
                master.mainMenu.Music(true);
                master.audio.Music(true);
                break;

            case "sfx":
                master.mainMenu.Sfx(false);
                master.audio.sfxOn = false;
                PlayerPrefs.SetInt("sfx", 0);
                break;

            case "sfx_mute":
                master.mainMenu.Sfx(true);
                master.audio.sfxOn = true;
                PlayerPrefs.SetInt("sfx", 1);
                break;

            case "costumes_next":
                master.costumeManager.NextCostume();
                break;
            
            case "costumes_prev":
                master.costumeManager.PreviousCostume();
                break;

            case "costumes_select":
                master.costumeManager.SetCostume();
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

            default:
                break;
        }
    }
    
}
