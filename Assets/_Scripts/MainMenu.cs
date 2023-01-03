using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour {
    public MasterManager master;
    public GameObject settingsUI, mainUI;
    public GameObject musicButton, musicButtonMuted, sfxButton, sfxButtonMuted;
    public GameObject yeti;
    // costumes
    public GameObject costumesListPrefab;
    private List<Costume> costumesList;
    // DEBUG
    public GameObject highscore_text, kills_text;

    private void Start()
    {


        // get master
        master = GameObject.Find("MASTER_MANAGER").GetComponent<MasterManager>();
        master.SceneChanged();

        // set chosen costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        yeti.GetComponent<SpriteRenderer>().sprite = costumesList[master.playerData.GetCostume()].both;

        // set audio settings
        Music(master.playerData.GetMusic());
        Sfx(master.playerData.GetSfx());

        // DEBUG
        highscore_text.GetComponent<TextMeshPro>().text = master.playerData.GetHighScore().ToString();
        kills_text.GetComponent<TextMeshPro>().text = master.playerData.GetKills().ToString();
    }

    public void ShowSettingsUI(bool on) {
        settingsUI.SetActive(on);
    }

    public void ShowMainUI(bool on) {
        mainUI.SetActive(on);
    }

    public void Music(bool value)
    {
        master.playerData.SetMusic(value);

        musicButton.SetActive(value);
        musicButtonMuted.SetActive(!value);

    }

    public void Sfx(bool value)
    {
        master.playerData.SetSfx(value);

        sfxButton.SetActive(value);
        sfxButtonMuted.SetActive(!value);

        

    }
}