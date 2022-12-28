using UnityEngine;
using System.Collections;
using TMPro;

public class GameOver : GameElement {

    public bool gameOver;
    public GameObject gameOver_UI, finalScoreLabel, highScoreLabel, newHighScoreLabel;
    public GameObject hiker, yeti, crown;

    public void SetGameOver() {
        gameOver = true;
        gameOver_UI.SetActive(true);

        // switch on high score items if true
        if (game.model.newHighScore) { 
            hiker.SetActive(false); 
        }

        SetScoreUI();
        game.model.allowInput = false;
        game.controller.DisableAllAnimations();
    }

    public void SetScoreUI() {
        // if 0 then use the letter 'o' instead, 0 looks like an 8 with chosen font
        finalScoreLabel.GetComponent<TextMeshPro>().text = game.model.score == 0 ? "o" : game.model.score.ToString();
        highScoreLabel.GetComponent<TextMeshPro>().text = game.model.highScore == 0 ? "o" : game.model.highScore.ToString();
    }

    public void ChangeSprites() {
        if (!game.model.newHighScore) {
            StartCoroutine(NoHighScore());
        } else {
            StartCoroutine(HighScore());
        }
    }

    IEnumerator NoHighScore() {
        yeti.GetComponent<SpriteRenderer>().sprite = game.yeti.yeti_dead;
        yield return new WaitForSeconds(1);
        hiker.GetComponent<SpriteRenderer>().sprite = game.hikers.hikerRed_smiling;
    }

    IEnumerator HighScore() {
        yeti.GetComponent<SpriteRenderer>().sprite = game.yeti.yeti_dead;
        yield return new WaitForSeconds(0);
        crown.SetActive(true);
        newHighScoreLabel.SetActive(true); 
    }
}