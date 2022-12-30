using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {
    public GameObject settingsUI, mainUI;
    public GameObject musicButton, musicButtonMuted, sfxButton, sfxButtonMuted;
    public GameObject yeti;
    // costumes
    public GameObject costumesListPrefab;
    private List<Costume> costumesList;
    //
    // add costumes ref here

    private void Start() {
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        // set chosen costume
        yeti.GetComponent<SpriteRenderer>().sprite = costumesList[PlayerPrefs.GetInt("costume")].both;
        // set audio settings
        if (PlayerPrefs.GetInt("music") == 0)
        {
            Music(false);
        }
        if (PlayerPrefs.GetInt("sfx") == 0)
        {
            Sfx(false);
        }
    }

    public void ShowSettingsUI(bool on) {
        settingsUI.SetActive(on);
    }

    public void ShowMainUI(bool on) {
        mainUI.SetActive(on);
    }

    public void Music(bool value)
    {
        int onOff = value ? onOff = 1 : onOff = 0; 
        PlayerPrefs.SetInt("music", onOff);

        musicButton.SetActive(value);
        musicButtonMuted.SetActive(!value);

        GameObject audio = GameObject.Find("Audio");
        if (value)
            audio.GetComponent<AudioSource>().Play();
        else
            audio.GetComponent<AudioSource>().Pause();
    }

    public void Sfx(bool value)
    {
        int onOff = value ? onOff = 1 : onOff = 0;
        PlayerPrefs.SetInt("sfx", onOff);

        sfxButton.SetActive(value);
        sfxButtonMuted.SetActive(!value);

        

    }
}