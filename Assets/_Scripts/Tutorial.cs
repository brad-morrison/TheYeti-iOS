using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : TheYeti
{
    public GameObject leftArrow, rightArrow;
    public BoxCollider2D leftCollider, rightCollider;

    public void ActivateTutorial()
    {
        this.gameObject.SetActive(true); // activate tutorial objects
        GM.gameManager.allowInput = false; // disable game input
        GM.gameManager.LifebarState(false); // pause the lifebar from scaling down
        GM.gameManager.allowGoldMode = false; // stop gold mode from spawning

        // activate correct side of tutorial items
        if (!GM.gameManager.hikers.activeHiker.GetComponent<Hiker>().left)
        {
            leftArrow.SetActive(true);
            leftCollider.enabled = true;
        }
        else
        {
            rightArrow.SetActive(true);
            rightCollider.enabled = true;
        }
    }

    // on taping the correct side, close tutorial and start game
    private void OnMouseDown()
    {
        // hide tutorial
        this.gameObject.SetActive(false);
        // switch input back on
        GM.gameManager.allowInput = true;
        // make sound
        GM.audio.PlaySound(GM.audio.pop);
        // turn on lifebar
        GM.gameManager.LifebarState(true);
        // allow goldmode to spawn
        GM.gameManager.allowGoldMode = true; 
    }
}
