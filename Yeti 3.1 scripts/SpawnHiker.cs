using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHiker : MonoBehaviour
{
    public GameObject greenHiker;
    public GameObject redHiker;
    public GameObject hikerGreen_dead, hikerRed_dead;

    public GameObject left1, left2, left3, left4, right1, right2, right3, right4; // target objects used for position

    public GameObject SpawnAt(int level)
    {
        GameObject newHiker;
        bool left;
        GameObject target = null;
        GameObject color;
        int randSide = Random.Range(0, 100);
        int randColor = Random.Range(0, 100);

        if (randSide < 50)
            left = true;
        else
            left = false;

        if (randColor < 50)
            color = redHiker;
        else 
            color = greenHiker;

        switch (level)
        {
            case 0:
                if (left)
                    target = left1;
                else
                    target = right1;
                break;
            case 1:
                if (left)
                    target = left2;
                else
                    target = right2;
                break;
            case 2:
                if (left)
                    target = left3;
                else
                    target = right3;
                break;
            case 3:
                if (left)
                    target = left4;
                else
                    target = right4;
                break;
        }

        newHiker = Instantiate(color, target.transform.position, Quaternion.identity);
        return newHiker;
        
    }
    public GameObject Spawn()
    {
        GameObject newHiker;
        int randSide = Random.Range(0, 100);
        int randColor = Random.Range(0, 100);
        string color = "";

        if (randColor < 50)
            color = "red";
        else
            color = "green";
            
        if (randSide < 50)
            newHiker = SpawnHikerLeft(color);
        else
            newHiker = SpawnHikerRight(color);

        return newHiker;
    }

    public GameObject SpawnHikerLeft(string type)
    {
        GameObject newHiker;

        if (type == "red")
        {
            newHiker = Instantiate(redHiker, left4.transform.position, Quaternion.identity);
        }
        else
        {
            newHiker = Instantiate(greenHiker, left4.transform.position, Quaternion.identity);
        }

        newHiker.GetComponent<Hiker>().side = "left";
        return newHiker;
    }

    public GameObject SpawnHikerRight(string type)
    {
        GameObject newHiker;

        if (type == "red")
        {
            newHiker = Instantiate(redHiker, right4.transform.position, Quaternion.identity);
        }
        else
        {
            newHiker = Instantiate(greenHiker, right4.transform.position, Quaternion.identity);
        }

        newHiker.GetComponent<Hiker>().side = "right";
        return newHiker;
    }

    public void SpawnDeadHiker(string side, GameObject hikerToKill)
    {
        GameObject deadHiker;

        if (side == "left")
        {
            if (hikerToKill.GetComponent<Hiker>().color == "red")
                deadHiker = Instantiate(hikerRed_dead, left1.transform.position, Quaternion.identity);
            else
                deadHiker = Instantiate(hikerGreen_dead, left1.transform.position, Quaternion.identity);
        }
        else
        {
            if (hikerToKill.GetComponent<Hiker>().color == "red")
                deadHiker = Instantiate(hikerRed_dead, right1.transform.position, Quaternion.identity);
            else
                deadHiker = Instantiate(hikerGreen_dead, right1.transform.position, Quaternion.identity);

            deadHiker.GetComponent<Animator>().SetBool("left", false);
        }
    }
}
