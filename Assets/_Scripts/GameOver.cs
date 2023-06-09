using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Timeline;
using UnityEngine.SocialPlatforms.Impl;

public class GameOver : TheYeti {

    public bool gameOver;
    public GameObject gameOver_UI, finalScoreLabel, highScoreLabel, newHighScoreLabel;
    public GameObject hiker, yeti, crown;

    public void SetGameOver() {

        gameOver = true;
        gameOver_UI.SetActive(true);
        GM.audio.PlaySound(GM.audio.gameOver);
        GM.audio.PlaySoundAfter(GM.audio.hit, 1.1f);
        GM.gameManager.isGameOver = true;
        // setkills
        int totalKills = GM.playerData.GetKills();
        GM.playerData.SetKills(totalKills + GM.gameManager.totalKills_counter);
        

        // switch on high score items if true
        if (GM.gameManager.newHighScore) { 
            hiker.SetActive(false); 
        }

        SetScoreUI();
        GM.gameManager.allowInput = false;
        GM.gameManager.DisableAllAnimations();
    }

    public void SetScoreUI() {
        // if 0 then use the letter 'o' instead, 0 looks like an 8 with chosen font
        finalScoreLabel.GetComponent<TextMeshPro>().text = GM.gameManager.score == 0 ? "o" : GM.gameManager.score.ToString();
        highScoreLabel.GetComponent<TextMeshPro>().text = GM.gameManager.highScore == 0 ? "o" : GM.gameManager.highScore.ToString();
    }

    public void ChangeSprites() {
        if (!GM.gameManager.newHighScore) {
            StartCoroutine(NoHighScore());
        } else {
            StartCoroutine(HighScore());
        }
    }

    // Coroutines
    //
    
    IEnumerator NoHighScore() {
        GM.audio.PlaySoundAfter(GM.audio.pop, 1);
        yeti.GetComponent<SpriteRenderer>().sprite = GM.gameManager.yeti.currentCostume.dead;
        yield return new WaitForSeconds(1);
        hiker.GetComponent<SpriteRenderer>().sprite = GM.gameManager.hikers.hikerRed_smiling;
    }

    IEnumerator HighScore() {
        yeti.GetComponent<SpriteRenderer>().sprite = GM.gameManager.yeti.currentCostume.dead;
        yield return new WaitForSeconds(0);
        crown.SetActive(true);
        GM.audio.PlaySound(GM.audio.crown);
        newHighScoreLabel.SetActive(true); 
    }
}
