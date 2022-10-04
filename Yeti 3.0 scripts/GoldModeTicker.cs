using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModeTicker : MonoBehaviour
{
    public GameObject goldFace;
    GameController gameController;
    Score score;
    public float leftX, rightX;
    int scoreAtLastGold = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("scripts").GetComponent<GameController>();
        score = GameObject.Find("scripts").GetComponent<Score>();
    }

    public void CheckForGoldChance()
    {
        if (GoldRoll() && gameController.goldModeActivated == false && gameController.dead == false)
            SpawnFace();
    }
    bool GoldRoll()
    {
        if ((score.scoreCount - scoreAtLastGold) > 10) // if score has advanced by 10
        {
            int roll = Random.Range(0, 4);
            if (roll == 0)
            {
                scoreAtLastGold = score.scoreCount;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void SpawnFace()
    {
        float y = ChooseY();
        
        if (RandPosition() == "left")
        {
            Instantiate(goldFace, new Vector3(leftX, y, -4.8f), Quaternion.identity);
            goldFace.GetComponent<MoveObject>().direction = "left";
        }
        else
        {
            Instantiate(goldFace, new Vector3(rightX, y, -4.8f), Quaternion.identity);
            goldFace.GetComponent<MoveObject>().direction = "right";
        }
        
    }

    float ChooseY()
    {
        return Random.Range(2.7f, -2.5f);
    }

    public string RandPosition()
    {
        int roll = Random.Range(0,2);
        if (roll == 0)
            return "left";
        else
            return "right";
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
