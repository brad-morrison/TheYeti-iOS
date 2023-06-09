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

    private void Awake() {

         GM.SceneChanged();

        // get score data
        highScore = GM.playerData.GetHighScore();
        totalKills = GM.playerData.GetKills();

        // set to current costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        currentCostume = costumesList[GM.playerData.GetCostume()];
        costumeIndex = GM.playerData.GetCostume();
        ShowCostume(currentCostume);


        //SetCostume();

        

        RefreshButtons();

        // animate yeti
        StartCoroutine(YetiAnimate());
    }

    public void NextCostume() {

        // move if button is active
        if (rightButton.GetComponent<Button>().active)
        {
            // show text
            scoreText.active = true;
            killsText.active = true;

            costumeIndex++;
            RefreshButtons();
            
            // show new costume
            currentCostume = costumesList[costumeIndex];
            ShowCostume(currentCostume);
        }

    }

    public void PreviousCostume() {
        
        // move if button is active
        if (leftButton.GetComponent<Button>().active)
        {
            // show text
            scoreText.active = true;
            killsText.active = true;

            costumeIndex--;
            RefreshButtons();
            
            // show new costume
            currentCostume = costumesList[costumeIndex];
            ShowCostume(currentCostume);
        }
    }

    public void ShowCostume(Costume costume) {

        // hide info if base yeti
        if (costumeIndex == 0)
        {
            scoreText.active = false;
            killsText.active = false;
            
        }

        // reset lock things
        yeti.GetComponent<SpriteRenderer>().color = Color.white;
        lockedText.SetActive(false);
        unlockedText.SetActive(true);
        unlockAllButton.SetActive(false);
        selectButton.GetComponent<Button>().Grey(false);

        // set generals
        scoreText.GetComponent<TextMeshPro>().text = costume.best.ToString();
        killsText.GetComponent<TextMeshPro>().text = costume.kills.ToString();
        nameText.GetComponent<TextMeshPro>().text = costume.name.ToString();

        // set lockables
        yeti.GetComponent<SpriteRenderer>().sprite = costume.both;

        // do things if costume is locked
        if (IsLocked(costume)) {
            // blackout sprite
            yeti.GetComponent<SpriteRenderer>().color = Color.black;
            // show locked text
            lockedText.SetActive(true);
            // hide unlocked text
            unlockedText.SetActive(false);
            // show unlockAll button
            unlockAllButton.SetActive(true);
            // lock select button
            selectButton.GetComponent<Button>().Grey(true);
        } 
    }

    public bool SetCostume() {

        if (!IsLocked(currentCostume))
        {
            // set player pref
            GM.playerData.SetCostume(costumeIndex);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool IsLocked(Costume costume) {

        if(highScore >= costume.best || totalKills >= costume.kills)
        {
            return false;
        }

        return true;
    }

    public void RefreshButtons() {

        // grey out right button if no other costumes
        if (costumeIndex == costumesList.Count-1) {
            rightButton.GetComponent<Button>().Grey(true);
        } else {
            rightButton.GetComponent<Button>().Grey(false);
        }

        // grey out left button if at 0
        if (costumeIndex == 0) {
            leftButton.GetComponent<Button>().Grey(true);
        } else {
            leftButton.GetComponent<Button>().Grey(false);
        }
    }

    public IEnumerator YetiAnimate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.idle2;
            yield return new WaitForSeconds(0.3f);
            yeti.GetComponent<SpriteRenderer>().sprite = currentCostume.idle1;
        }
    }

}