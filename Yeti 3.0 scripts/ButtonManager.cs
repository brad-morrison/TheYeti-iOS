using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    Costumes costumes;
    CostumesUI costumesUI;
    Audio audio;
    public GameObject settingsPage;

    private void Start() 
    {
        audio = GameObject.Find("Audio").GetComponent<Audio>();
        costumes = GameObject.Find("costumes_list(Clone)").GetComponent<Costumes>();
        costumesUI = GameObject.Find("scripts").GetComponent<CostumesUI>();
        
    }
    public void ButtonPress(string buttonName)
    {
        switch(buttonName)
        {
            case "play_button":
                // load play level
                Application.LoadLevel("game");
                break;

            case "leaderBoard_button":
                //open leaderboards
                break;

            case "costumes_button":
                // load costumes level
                Application.LoadLevel("CostumeScene");
                break;

            case "facebook":
                break;

            case "twitter":
                break;

            case "share":
                break;

            case "left_button":
                costumesUI.PressedLeft();
                break;

            case "right_button":
                costumesUI.PressedRight();
                break;
            
            case "select_button":
                if(!costumesUI.locked)
                {
                    PlayerPrefs.SetInt("costume", costumesUI.currentIndex);
                    Application.LoadLevel("menu");
                }
                break;

            case "unlockAll":
                break;
            
            case "settings_button":
                settingsPage.SetActive(true);
                break;

            case "close_menu":
                settingsPage.SetActive(false);
                break;

            case "music":
                Debug.Log("music off");
                GameObject button1 = GameObject.Find("music");
                GameObject button2 = GameObject.Find("music_off");
                button1.GetComponent<SpriteRenderer>().enabled = false;
                button1.GetComponent<BoxCollider2D>().enabled = false;
                button2.GetComponent<SpriteRenderer>().enabled = true;
                button2.GetComponent<BoxCollider2D>().enabled = true;
                break;
            
            case "music_off":
                Debug.Log("music on");
                GameObject button3 = GameObject.Find("music");
                GameObject button4 = GameObject.Find("music_off");
                button3.GetComponent<SpriteRenderer>().enabled = true;
                button3.GetComponent<BoxCollider2D>().enabled = true;
                button4.GetComponent<SpriteRenderer>().enabled = false;
                button4.GetComponent<BoxCollider2D>().enabled = false;
                
                break;

            case "sfx":
                Debug.Log("music off");
                audio.on = 0;
                GameObject button5 = GameObject.Find("sfx");
                GameObject button6 = GameObject.Find("sfx_off");
                button5.GetComponent<SpriteRenderer>().enabled = false;
                button5.GetComponent<BoxCollider2D>().enabled = false;
                button6.GetComponent<SpriteRenderer>().enabled = true;
                button6.GetComponent<BoxCollider2D>().enabled = true;
                PlayerPrefs.SetInt("sfx", 0);
                break;
            
            case "sfx_off":
                audio.on = 1;
                Debug.Log("music on");
                GameObject button7 = GameObject.Find("sfx");
                GameObject button8 = GameObject.Find("sfx_off");
                button7.GetComponent<SpriteRenderer>().enabled = true;
                button7.GetComponent<BoxCollider2D>().enabled = true;
                button8.GetComponent<SpriteRenderer>().enabled = false;
                button8.GetComponent<BoxCollider2D>().enabled = false;
                PlayerPrefs.SetInt("sfx", 1);
                break;
        }
    }
}
