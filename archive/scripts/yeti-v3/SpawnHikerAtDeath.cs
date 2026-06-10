using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHikerAtDeath : MonoBehaviour
{
    public GameObject standingHiker;
    GameObject hiker;
    public Vector3 left = new Vector3();
    public Vector3 right = new Vector3();
    public Sprite red, green;
    
    public void SpawnHiker(string side, string color)
    {
        if (side == "left")
        { 
            hiker = Instantiate(standingHiker, left, Quaternion.identity);
            hiker.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {   
            hiker = Instantiate(standingHiker, right, Quaternion.identity);
        }

        if (color == "red")
        {
            hiker.GetComponent<SpriteRenderer>().sprite = red;
        }
        else
        {
            hiker.GetComponent<SpriteRenderer>().sprite = green;
        }
    }
}
