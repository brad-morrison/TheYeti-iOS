using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Costumes : GameElement {
    public GameObject yeti, nameText, scoreText, killsText;
    public GameObject unlockedText, lockedText;
    public GameObject leftButton, rightButton, selectButton, unlockAllButton;
    public List<Costume> costumesList = new List<Costume>();
    public Costume currentCostume;
    public int costumeIndex = 0;

    private void Awake() {
        // set to current costume
        currentCostume = costumesList[costumeIndex];
        SetCostume(currentCostume);

        RefreshButtons();
    }

    public void NextCostume() {

        // move if button is active
        if (rightButton.GetComponent<Button>().active)
        {
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
            costumeIndex--;
            RefreshButtons();
            
            // show new costume
            currentCostume = costumesList[costumeIndex];
            ShowCostume(currentCostume);
        }
    }

    public void ShowCostume(Costume costume) {
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

    public void SetCostume(Costume costume) {
        // set costume in model
        game.yeti.currentCostume = costume;
        // set playerpref
        PlayerPrefs.SetInt("costume", costumeIndex);
    }

    public bool IsLocked(Costume costume) {

        if(game.model.highScore > costume.best || game.model.totalKills > costume.kills)
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

    

}