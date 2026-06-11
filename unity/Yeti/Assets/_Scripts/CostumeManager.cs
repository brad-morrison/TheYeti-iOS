using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CostumeManager : TheYeti {
    public GameObject yeti, nameText, scoreText, killsText;
    public GameObject unlockedText, lockedText;
    public GameObject leftButton, rightButton, selectButton, unlockAllButton;
    // costumes
    public GameObject costumesListPrefab;
    private List<Costume> costumesList;
    //
    public Costume currentCostume;
    public int costumeIndex;
    // get from prefs
    public int highScore, totalKills;
    private SpriteRenderer yetiRenderer;
    private TextMeshPro nameLabel;
    private TextMeshPro scoreLabel;
    private TextMeshPro killsLabel;
    private Button leftButtonControl;
    private Button rightButtonControl;
    private Button selectButtonControl;

    private void Awake() {
        yetiRenderer = yeti.GetComponent<SpriteRenderer>();
        nameLabel = nameText.GetComponent<TextMeshPro>();
        scoreLabel = scoreText.GetComponent<TextMeshPro>();
        killsLabel = killsText.GetComponent<TextMeshPro>();
        leftButtonControl = leftButton.GetComponent<Button>();
        rightButtonControl = rightButton.GetComponent<Button>();
        selectButtonControl = selectButton.GetComponent<Button>();

        // get score data
        highScore = GM.playerData.GetHighScore();
        totalKills = GM.playerData.GetKills();

        // set to current costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        currentCostume = costumesList[GM.playerData.GetCostume()];
        costumeIndex = GM.playerData.GetCostume();
        ShowCostume(currentCostume);

        RefreshButtons();

        // animate yeti
        StartCoroutine(YetiAnimate());
    }

    public void NextCostume() {

        // move if button is active
        if (!rightButtonControl.active)
            return;

        ShowRequirementText(true);
        ShowCostumeAt(costumeIndex + 1);
    }

    public void PreviousCostume() {
        
        // move if button is active
        if (!leftButtonControl.active)
            return;

        ShowRequirementText(true);
        ShowCostumeAt(costumeIndex - 1);
    }

    private void ShowCostumeAt(int index)
    {
        costumeIndex = index;
        RefreshButtons();

        currentCostume = costumesList[costumeIndex];
        ShowCostume(currentCostume);
    }

    public void ShowCostume(Costume costume) {

        // hide info if base yeti
        if (costumeIndex == 0)
        {
            ShowRequirementText(false);
        }

        ApplyUnlockedState();
        SetCostumeLabels(costume);
        yetiRenderer.sprite = costume.both;

        if (IsLocked(costume)) {
            ApplyLockedState();
        }
    }

    private void ShowRequirementText(bool isVisible)
    {
        scoreText.SetActive(isVisible);
        killsText.SetActive(isVisible);
    }

    private void SetCostumeLabels(Costume costume)
    {
        scoreLabel.text = costume.best.ToString();
        killsLabel.text = costume.kills.ToString();
        nameLabel.text = costume.name.ToString();
    }

    private void ApplyUnlockedState()
    {
        yetiRenderer.color = Color.white;
        lockedText.SetActive(false);
        unlockedText.SetActive(true);
        unlockAllButton.SetActive(false);
        selectButtonControl.Grey(false);
    }

    private void ApplyLockedState()
    {
        yetiRenderer.color = Color.black;
        lockedText.SetActive(true);
        unlockedText.SetActive(false);
        unlockAllButton.SetActive(true);
        selectButtonControl.Grey(true);
    }

    public bool SetCostume() {

        if (IsLocked(currentCostume))
            return false;

        // set player pref
        GM.playerData.SetCostume(costumeIndex);
        return true;
    }

    public bool IsLocked(Costume costume) {
        return highScore < costume.best && totalKills < costume.kills && GM.playerData.globalUnlock != 1;
    }

    public void RefreshButtons() {
        rightButtonControl.Grey(costumeIndex == costumesList.Count-1);
        leftButtonControl.Grey(costumeIndex == 0);
    }


    public IEnumerator YetiAnimate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            yetiRenderer.sprite = currentCostume.idle2;
            yield return new WaitForSeconds(0.3f);
            yetiRenderer.sprite = currentCostume.idle1;
        }
    }

}
