using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Timeline;

public class GameOver : MonoBehaviour {

    public bool gameOver;
    public GameObject gameOver_UI, finalScoreLabel, highScoreLabel, newHighScoreLabel;
    public GameObject hiker, yeti, crown;
    public GameManager manager;
    public Audio audio;

    private void Start()
    {
        audio = GameObject.Find("Audio").GetComponent<Audio>();
    }

    public void SetGameOver() {
        gameOver = true;
        gameOver_UI.SetActive(true);
        audio.PlaySound(audio.gameOver);
        audio.PlaySoundAfter(audio.hit, 1.1f);
        manager.isGameOver = true;
        iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)5);
        // setkills
        int totalKills = manager.master.playerData.GetKills();
        manager.master.playerData.SetKills(totalKills + manager.totalKills_counter);
        

        // switch on high score items if true
        if (manager.newHighScore) { 
            hiker.SetActive(false); 
        }

        SetScoreUI();
        manager.allowInput = false;
        manager.DisableAllAnimations();
    }

    public void SetScoreUI() {
        // if 0 then use the letter 'o' instead, 0 looks like an 8 with chosen font
        finalScoreLabel.GetComponent<TextMeshPro>().text = manager.score == 0 ? "o" : manager.score.ToString();
        highScoreLabel.GetComponent<TextMeshPro>().text = manager.highScore == 0 ? "o" : manager.highScore.ToString();
    }

    public void ChangeSprites() {
        if (!manager.newHighScore) {
            StartCoroutine(NoHighScore());
        } else {
            StartCoroutine(HighScore());
        }
    }

    IEnumerator NoHighScore() {
        audio.PlaySoundAfter(audio.pop, 1);
        yeti.GetComponent<SpriteRenderer>().sprite = manager.yeti.currentCostume.dead;
        yield return new WaitForSeconds(1);
        hiker.GetComponent<SpriteRenderer>().sprite = manager.hikers.hikerRed_smiling;
    }

    IEnumerator HighScore() {
        yeti.GetComponent<SpriteRenderer>().sprite = manager.yeti.currentCostume.dead;
        yield return new WaitForSeconds(0);
        crown.SetActive(true);
        audio.PlaySound(audio.crown);
        newHighScoreLabel.SetActive(true); 
    }
}
