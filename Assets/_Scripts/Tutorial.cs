using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : TheYeti
{
    public GameObject leftArrow, rightArrow;

    public void ActivateTutorial()
    {
        this.gameObject.SetActive(true);
        GM.gameManager.allowInput = false;

        if (!GM.gameManager.hikers.activeHiker.GetComponent<Hiker>().left)
        {
            leftArrow.SetActive(true);
        }
        else
        {
            rightArrow.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
        GM.gameManager.allowInput = true;
        GM.audio.PlaySound(GM.audio.pop);
    }
}
