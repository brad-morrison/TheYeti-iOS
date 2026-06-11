using UnityEngine;
using System.Collections;
using TMPro;

public class GameOver : TheYeti {

    public bool gameOver;
    public GameObject gameOver_UI, finalScoreLabel, highScoreLabel, newHighScoreLabel, killsLabel;
    public GameObject hiker, yeti, crown;
    private TextMeshPro finalScoreText;
    private TextMeshPro highScoreText;
    private TextMeshPro killsText;
    private SpriteRenderer hikerRenderer;
    private SpriteRenderer yetiRenderer;

    private void Awake()
    {
        finalScoreText = finalScoreLabel.GetComponent<TextMeshPro>();
        highScoreText = highScoreLabel.GetComponent<TextMeshPro>();
        killsText = killsLabel.GetComponent<TextMeshPro>();
        hikerRenderer = hiker.GetComponent<SpriteRenderer>();
        yetiRenderer = yeti.GetComponent<SpriteRenderer>();
    }

    public void SetGameOver() {

        gameOver = true;
        gameOver_UI.SetActive(true);
        GM.audio.PlaySound(GM.audio.gameOver);
        GM.audio.PlaySoundAfter(GM.audio.hit, 1.1f);
        GM.gameManager.isGameOver = true;
        GM.gameManager.frenzyMode.HideAllUI(true);
        

        // switch on high score items if true
        if (GM.gameManager.newHighScore) { 
            hiker.SetActive(false); 
        }

        SetScoreUI();
        GM.gameManager.allowInput = false;
        GM.gameManager.DisableAllAnimations();

        GM.gameManager.frenzyMode.HideAllUI(true);
    }

    public void SetScoreUI() {
        // if 0 then use the letter 'o' instead, 0 looks like an 8 with chosen font
        finalScoreText.text = GameManager.FormatScore(GM.gameManager.score);
        highScoreText.text = GameManager.FormatScore(GM.gameManager.highScore);
        killsText.text = GameManager.FormatScore(GM.playerData.GetKills());
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
        yetiRenderer.sprite = GM.gameManager.yeti.currentCostume.dead;
        yield return new WaitForSeconds(1);
        hikerRenderer.sprite = GM.gameManager.hikers.hikerRed_smiling;
    }

    IEnumerator HighScore() {
        yetiRenderer.sprite = GM.gameManager.yeti.currentCostume.dead;
        yield return new WaitForSeconds(0);
        crown.SetActive(true);
        GM.audio.PlaySound(GM.audio.crown);
        newHighScoreLabel.SetActive(true); 
    }
}
