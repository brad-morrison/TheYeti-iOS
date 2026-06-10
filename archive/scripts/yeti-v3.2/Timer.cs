using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    GameController gameController;
    public float difficultyMultiplier;
    public float punchAmount;
    public GameObject timer_scroll;
    public Texture grey, blue, red;
    float yScale;
    float scaleSpeed;
    bool stop = false;
    bool flashing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("scripts").GetComponent<GameController>();
        yScale = gameObject.transform.localScale.y;
    }

    public void PunchScale()
    {
        if (transform.localScale.x + punchAmount < 0)
        {
            gameObject.transform.localScale = new Vector2(0, yScale);
        }
        else
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + punchAmount, yScale);
        }

        StartCoroutine(flashTexture());
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop && gameObject.transform.localScale.x < 0.74f && !gameController.goldModeActivated)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.74f, yScale, 1), difficultyMultiplier * Time.deltaTime);
        }

        if (gameObject.transform.localScale.x > 0.5 && !flashing)
        {
            timer_scroll.GetComponent<Renderer>().material.mainTexture = red;
        }

        if (gameObject.transform.localScale.x < 0.5 && !flashing)
        {
            timer_scroll.GetComponent<Renderer>().material.mainTexture = blue;
        }

        if (gameObject.transform.localScale.x == 0.74f)
        {
            if (gameController.dead == false)
                gameController.Death();
        }
        
    }

    public IEnumerator flashTexture()
    {
        flashing = true;
        Texture originalTexture = timer_scroll.GetComponent<Renderer>().material.mainTexture;
        timer_scroll.GetComponent<Renderer>().material.mainTexture = grey;
        yield return new WaitForSeconds(0.08f);
        timer_scroll.GetComponent<Renderer>().material.mainTexture = originalTexture;
        flashing = false;
    }
}
