using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {
    public GameObject settingsUI, mainUI;
    public GameObject yeti;
    // add costumes ref here

    private void Start() {
        // set chosen costume
        //yeti.GetComponent<SpriteRenderer>().sprite = game.costumes.costumesList[PlayerPrefs.GetInt("costume")].both;
        
        // costume ref to add - yeti.GetComponent<SpriteRenderer>().sprite = game.costumes.costumesList[2].both;
        Debug.Log(PlayerPrefs.GetInt("costume"));
    }

    public void ShowSettingsUI(bool on) {
        settingsUI.SetActive(on);
    }

    public void ShowMainUI(bool on) {
        mainUI.SetActive(on);
    }
}