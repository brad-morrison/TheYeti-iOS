using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInit : MonoBehaviour
{
    Audio audio;
    public GameObject sfx_on, sfx_off, music_on, music_off;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("Audio").GetComponent<Audio>();

        if (audio.on == 0)
        {
            Debug.Log("setting button to off");
            sfx_off.GetComponent<SpriteRenderer>().enabled = true;
            sfx_off.GetComponent<BoxCollider2D>().enabled = true;
            sfx_on.GetComponent<SpriteRenderer>().enabled = false;
            sfx_on.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Debug.Log("setting button to on");
            sfx_off.GetComponent<SpriteRenderer>().enabled = false;
            sfx_off.GetComponent<BoxCollider2D>().enabled = false;
            sfx_on.GetComponent<SpriteRenderer>().enabled = true;
            sfx_on.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
