using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    Audio audio;
    public GameObject scoreUI, high_scoreUI;
    public GameObject backdrop, buttons, hiker, yeti, goldUI;
    public GameObject target_backdrop, target_buttons, target_hiker, target_yeti;
    public float backdropSpeed, buttonsSpeed, hikerSpeed, yetiSpeed;
    public float buttonsWait, hikerWait, yetiWait;
    public Sprite hikerFront, hikerWin, yetiLose, yetiWin;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("Audio").GetComponent<Audio>();
    }

    public void OpenUI()
    {
        goldUI.SetActive(false);
        SetUIText();
        Backdrop_Drop();
        Invoke("Buttons_MoveIn", buttonsWait);
        Invoke("Hiker_Drop", hikerWait);
        Invoke("Yeti_MoveIn", yetiWait);
        StartCoroutine(HikerSprites());
    }

    public void SetUIText()
    {
        scoreUI.GetComponent<TextMeshPro>().text = gameObject.GetComponent<Score>().scoreCount.ToString(); 
        high_scoreUI.GetComponent<TextMeshPro>().text = gameObject.GetComponent<Score>().highScore.ToString();
    }
    public void Backdrop_Drop()
    {
        iTween.MoveTo(backdrop, iTween.Hash("speed",backdropSpeed,"easeType",iTween.EaseType.easeOutBounce, "position",target_backdrop.transform.position));
    }

    public void Hiker_Drop()
    {
        iTween.MoveTo(hiker, iTween.Hash("speed",hikerSpeed,"easeType",iTween.EaseType.easeOutBounce, "position",target_hiker.transform.position));
        audio.Play(audio.uiDrop);
    }

    public void Buttons_MoveIn()
    {
        iTween.MoveTo(buttons, target_buttons.transform.position, buttonsSpeed);
    }

    public void Yeti_MoveIn()
    {
        iTween.MoveTo(yeti, target_yeti.transform.position, yetiSpeed);
    }

    public IEnumerator HikerSprites()
    {
        yield return new WaitForSeconds(3);
        hiker.GetComponent<SpriteRenderer>().sprite = hikerFront;
        yeti.GetComponent<SpriteRenderer>().sprite = yetiLose;
        yield return new WaitForSeconds(0.8f);
        audio.Play(audio.emote);
        hiker.GetComponent<SpriteRenderer>().sprite = hikerWin;
    }


}
