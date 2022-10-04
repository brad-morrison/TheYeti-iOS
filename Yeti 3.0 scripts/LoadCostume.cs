using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCostume : MonoBehaviour
{   
    Costumes costumes;
    YetiSprite yetiSprite;
    GameObject yeti;
    public GameObject costumesPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        costumes = InitCostumes();
        yeti = GameObject.Find("yeti");
        yetiSprite = yeti.GetComponent<YetiSprite>();
        SetCostume(PlayerPrefs.GetInt("costume", 0));
        RefreshCostume();
    }

    
    Costumes InitCostumes()
    {
        GameObject created = Instantiate(costumesPrefab);
        return created.GetComponent<Costumes>();
    }

    void SetCostume(int index)
    {
        yetiSprite.left = costumes.costumesList[index].sprites[0];
        yetiSprite.right = costumes.costumesList[index].sprites[1];
        yetiSprite.bothUp = costumes.costumesList[index].sprites[2];
        yetiSprite.bothDown_1 = costumes.costumesList[index].sprites[3];
        yetiSprite.bothDown_2 = costumes.costumesList[index].sprites[4];
        yetiSprite.death = costumes.costumesList[index].sprites[5];
    }

    void RefreshCostume()
    {
        if (SceneManager.GetActiveScene().name == "game")
            yeti.GetComponent<SpriteRenderer>().sprite = yetiSprite.bothUp;
        else
            yeti.GetComponent<SpriteRenderer>().sprite = yetiSprite.bothDown_1;
    }

    
}
