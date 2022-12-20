using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : GameElement
{
    public float punchAmount;
    public GameObject timer_scroller;
    public GameObject goldFrame;
    public Texture grey;
    public Texture blue;
    public Texture red;
    public Texture gold;
    Texture current;
    bool flashing;
    float startScale;
    float scalePreGoldmode;

    private void Start()
    {
        current = blue;
        flashing = false;
        startScale = transform.localScale.x;
    }

    public void SetTexture(Texture tex)
    {
        // change texture if different
        if (timer_scroller.GetComponent<Renderer>().material.mainTexture != tex)
        {
            timer_scroller.GetComponent<Renderer>().material.mainTexture = tex;
        }       
    }

    public void PunchScale()
    {
        if (!game.model.goldMode)
        {
            if (transform.localScale.x + punchAmount > 0)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + punchAmount, transform.localScale.y);
            }
            StartCoroutine(flashTexture());
        }

    }

    public void ActivateGoldMode()
    {
        // save scale of bar
        scalePreGoldmode = transform.localScale.x;
        // activate gold frame
        goldFrame.SetActive(true);
        // set texture of lifeBar
        SetTexture(gold);
        // set scale of bar to start
        transform.localScale = new Vector3(startScale, transform.localScale.y, transform.localScale.z);
        // change speed of texture scroll
        game.model.lifeBar_ScrollSpeed = game.model.lifeBar_ScrollSpeed * 2;
    }

    public void DeactivateGoldMode()
    {
        goldFrame.SetActive(false);
        SetTexture(current);
        transform.localScale = new Vector3(scalePreGoldmode, transform.localScale.y, transform.localScale.z);
        game.model.lifeBar_ScrollSpeed = game.model.lifeBar_ScrollSpeed / 2;
    }

    private void Update()
    {
        // scale by difficulty OR linearly if in goldmode
        if (!game.model.goldMode)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(3, transform.localScale.y, transform.localScale.z), game.model.difficultyMultiplier * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(3, transform.localScale.y, transform.localScale.z), game.model.goldModeLength * Time.deltaTime);
        }
        
        if (transform.localScale.x > 2.0f) { current = red; } else { current = blue; }

        if (!flashing && !game.model.goldMode) { SetTexture(current); }

        // when bar reaches 0
        if (transform.localScale.x > 2.944f)
        {
            if (game.model.goldMode)
            {
                game.controller.DeactivateGoldMode();
            }
            else
            {
                print("gameover");
            }
        }

        if (Input.GetKeyDown("space"))
        {
            game.controller.ActivateGoldMode();
        }
    }

    public IEnumerator flashTexture()
    {
        flashing = true;
        SetTexture(current);
        SetTexture(grey);
        yield return new WaitForSeconds(0.05f);
        SetTexture(current);
        flashing = false;
    }
}
