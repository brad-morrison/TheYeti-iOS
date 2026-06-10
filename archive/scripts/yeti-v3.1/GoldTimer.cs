using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTimer : MonoBehaviour
{
    GameController gameController;
    public float speed;
    float yScale;
    public bool timerOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("scripts").GetComponent<GameController>();
        yScale = gameObject.transform.localScale.y;
    }

    void Update()
    {
        if (gameObject.transform.localScale.x < 0.74f && timerOn)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.74f, yScale, 1), speed * Time.deltaTime);
        }
        
        if (gameObject.transform.localScale.x == 0.74f && gameController.goldModeActivated)
        {
            timerOn = false;
            gameController.EndGoldMode();
        }
        
    }

    
}

