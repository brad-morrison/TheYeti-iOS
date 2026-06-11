using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : TheYeti {
    public GameObject settingsUI, mainUI;
    public GameObject musicButton, musicButtonMuted, sfxButton, sfxButtonMuted;
    public GameObject yeti;
    // costumes
    public GameObject costumesListPrefab;
    private List<Costume> costumesList;
    // DEBUG
    public GameObject highscore_text, kills_text;
    private SpriteRenderer yetiRenderer;
    private TextMeshPro highScoreText;
    private TextMeshPro killsText;

    private void Awake()
    {
        yetiRenderer = yeti.GetComponent<SpriteRenderer>();
        highScoreText = highscore_text.GetComponent<TextMeshPro>();
        killsText = kills_text.GetComponent<TextMeshPro>();

        // set chosen costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        yetiRenderer.sprite = costumesList[GM.playerData.GetCostume()].idle1;
        StartCoroutine(YetiAnimate());

        // set audio settings
        Music(GM.playerData.GetMusic());
        Sfx(GM.playerData.GetSfx());

        

        // DEBUG
        highScoreText.text = GM.playerData.GetHighScore().ToString();
        killsText.text = GM.playerData.GetKills().ToString();
    }

    public void ShowSettingsUI(bool on) {
        settingsUI.SetActive(on);
    }

    public void ShowMainUI(bool on) {
        mainUI.SetActive(on);
    }

    public void Music(bool value)
    {
        GM.playerData.SetMusic(value);

        musicButton.SetActive(value);
        musicButtonMuted.SetActive(!value);

    }

    public void Sfx(bool value)
    {
        GM.playerData.SetSfx(value);

        sfxButton.SetActive(value);
        sfxButtonMuted.SetActive(!value);

    }

    public IEnumerator YetiAnimate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            yetiRenderer.sprite = costumesList[GM.playerData.GetCostume()].idle2;
            yield return new WaitForSeconds(0.3f);
            yetiRenderer.sprite = costumesList[GM.playerData.GetCostume()].idle1;
        }
    }
}
