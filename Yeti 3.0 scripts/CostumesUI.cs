using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostumesUI : MonoBehaviour
{
    public Costumes costumes;
    YetiSprite yetiSprite;
    public GameObject text_topBest, text_topKills, text_name, text_score, text_toKill;
    public GameObject static_score, static_toKill, static_unlocked;
    public GameObject yeti;
    public GameObject leftButton, rightButton, selectButton;
    public Sprite idle1, idle2;
    TextMeshPro tmp_topBest, tmp_topKills, tmp_name, tmp_score, tmp_toKill;
    public int currentIndex = 0, score, kills;
    public bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        tmp_topBest = text_topBest.GetComponent<TextMeshPro>();
        tmp_topKills = text_topKills.GetComponent<TextMeshPro>();
        tmp_name = text_name.GetComponent<TextMeshPro>();
        tmp_score = text_score.GetComponent<TextMeshPro>();
        tmp_toKill = text_toKill.GetComponent<TextMeshPro>();

        currentIndex = PlayerPrefs.GetInt("costume", 0);
        costumes = GameObject.Find("costumes_list(Clone)").GetComponent<Costumes>();
        yetiSprite = yeti.GetComponent<YetiSprite>();
        score = PlayerPrefs.GetInt("high_score", 0);
        //score = 500;
        kills = PlayerPrefs.GetInt("kills", 0);
        //kills = 10;
        SetPlayerPrefsUI();
        RefreshUI();
    }
    
    public void SetPlayerPrefsUI()
    {
        tmp_topBest.text = score.ToString();
        tmp_topKills.text = kills.ToString();
    }

    public void RefreshUI()
    {
        if(!CheckIfUnlocked(costumes.costumesList[currentIndex]))
        {
            SetToLocked();
            Set_Name(costumes.costumesList[currentIndex].name);
            Set_Score(costumes.costumesList[currentIndex].unlockScore.ToString());
            Set_ToKill(costumes.costumesList[currentIndex].unlockKills.ToString());
            Set_Sprite(currentIndex);
            yeti.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            SetToUnlocked();
            Set_Sprite(currentIndex);
            yeti.GetComponent<SpriteRenderer>().color = Color.white;
        }

        CheckForGreySprites();
    }

    public bool CheckIfUnlocked(Costume costume)
    {
        bool scoreCheck = false;
        bool killsCheck = false;

        if(costume.unlockScore <= score)
            scoreCheck = true;

        if (costume.unlockKills <= kills)
            killsCheck = true;

        if (scoreCheck || killsCheck)
            return true;
        else
            return false;
    }

    public void SetToUnlocked()
    {
        locked = false;
        tmp_score.text = "";
        tmp_toKill.text = "";
        static_score.GetComponent<SpriteRenderer>().enabled = false;
        static_toKill.GetComponent<SpriteRenderer>().enabled = false;
        static_unlocked.GetComponent<MeshRenderer>().enabled = true;
        selectButton.GetComponent<Button>().SetActive();
    }

    public void SetToLocked()
    {
        locked = true;
        static_score.GetComponent<SpriteRenderer>().enabled = true;
        static_toKill.GetComponent<SpriteRenderer>().enabled = true;
        static_unlocked.GetComponent<MeshRenderer>().enabled = false;
        selectButton.GetComponent<Button>().SetInActive();
    }

    public void PressedLeft()
    {
        if(currentIndex > 0)
        {
            currentIndex--;
            RefreshUI();
        }
    }
    public void PressedRight()
    {
        if(currentIndex+1 < costumes.costumesList.Count)
        {
            currentIndex++;
            RefreshUI();
        }
    }

    public void CheckForGreySprites()
    {
        if (currentIndex == 0)
        {
            leftButton.GetComponent<Button>().SetInActive();
        }
        else
        {
            leftButton.GetComponent<Button>().SetActive();
        }
        
        if (currentIndex+1 == costumes.costumesList.Count)
        {
            rightButton.GetComponent<Button>().SetInActive();
        }
        else
        {
            rightButton.GetComponent<Button>().SetActive();
        }
    }
    
    public void Set_Sprite(int index)
    {
        yetiSprite.left = costumes.costumesList[index].sprites[0];
        yetiSprite.right = costumes.costumesList[index].sprites[1];
        yetiSprite.bothUp = costumes.costumesList[index].sprites[2];
        yetiSprite.bothDown_1 = costumes.costumesList[index].sprites[3];
        yetiSprite.bothDown_2 = costumes.costumesList[index].sprites[4];
        yetiSprite.death = costumes.costumesList[index].sprites[5];
        yeti.GetComponent<SpriteRenderer>().sprite = yetiSprite.bothDown_1;
    }
    public void Set_TopBest(string newText)
    {
        tmp_topBest.text = newText;
    }
    
    public void Set_TopKills(string newText)
    {
        tmp_topKills.text = newText;
    }

    public void Set_Name(string newText)
    {
        tmp_name.text = newText;
    }

    public void Set_Score(string newText)
    {
        tmp_score.text = newText;
    }

    public void Set_ToKill(string newText)
    {
        tmp_toKill.text = newText;
    }

}
