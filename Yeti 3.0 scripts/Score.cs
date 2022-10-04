using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    GameController gameController;
    public int scoreCount = 0;
    public int highScore;
    int kills = 0;
    public GameObject scoreUI, scoreUI_shadow, score_parent;
    public Color gold;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("high_score", 0);
        gameController = GetComponent<GameController>();
    }

    public void ScoreAdd(int amount)
    {
        scoreCount = scoreCount + amount;
        CheckForNewHighScore();
        kills++;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreUI.GetComponent<TextMeshPro>().text = scoreCount.ToString();
        scoreUI_shadow.GetComponent<TextMeshPro>().text = scoreCount.ToString();

    }

    public void AddToKillsScore()
    {
        int currentKills = PlayerPrefs.GetInt("kills", 0);
        int newKills = currentKills + kills;
        PlayerPrefs.SetInt("kills", newKills);
    }

    public void CheckForNewHighScore()
    {
        if (gameController.newHighScore != true)
        {
            if (scoreCount > highScore)
            {
                gameController.SetNewHighScore();
            }
        }
    }

    public void PunchScale()
    {
        iTween.PunchScale(score_parent, new Vector3(0.1f, 0.1f,0.1f), 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
