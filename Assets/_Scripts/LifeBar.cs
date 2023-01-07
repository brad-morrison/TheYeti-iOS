using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public Audio audio;
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
    public bool animate;
    public GameManager manager;

    private void Start()
    {
        audio = GameObject.Find("Audio").GetComponent<Audio>();
        animate = true;
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
        if (!manager.goldMode.goldMode)
        {
            if (transform.localScale.x + punchAmount > 0)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + punchAmount, transform.localScale.y);
            }
            StartCoroutine(flashTexture());
        }

    }

    public void GoldMode()
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
        manager.lifeBar_ScrollSpeed = manager.lifeBar_ScrollSpeed * 2;
    }

    public void NormalMode()
    {
        goldFrame.SetActive(false);
        SetTexture(current);
        transform.localScale = new Vector3(scalePreGoldmode, transform.localScale.y, transform.localScale.z);
        manager.lifeBar_ScrollSpeed = manager.lifeBar_ScrollSpeed / 2;
    }

    private void Update()
    {
        // scale by difficulty OR linearly if in goldmode
        if (!manager.goldMode.goldMode && animate)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(3, transform.localScale.y, transform.localScale.z), manager.difficultyMultiplier * Time.deltaTime);
        }
        else if (manager.goldMode.goldMode && animate)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(3, transform.localScale.y, transform.localScale.z), manager.goldMode.goldModeLength * Time.deltaTime);
        }
        
        // turn red if below certain size
        if (transform.localScale.x > 2.0f) { current = red; audio.PlaySound(audio.timerLow); } else { current = blue; }

        if (!flashing && !manager.goldMode.goldMode) { SetTexture(current); }

        // when bar reaches 0
        // extra check to only run if gameover is false to avoid infinite loop
        if (transform.localScale.x > 2.944f && !manager.gameOver.gameOver)
        {
            if (manager.goldMode.goldMode)
            {
                manager.DeactivateGoldMode();
            }
            else
            {
                if (!manager.noTimerDeath) // for debug
                    manager.gameOver.SetGameOver();
            }
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
