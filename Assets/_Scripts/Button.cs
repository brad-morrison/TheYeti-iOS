using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Button : MonoBehaviour {
    public Audio audio;
    public Sprite on, on_inActive, off_inActive;
    Sprite off;
    public string function;
    public bool pressActive;
    public bool active; // greyed out or not

    private void Awake() {
        audio = GameObject.Find("Audio").GetComponent<Audio>();
        off = GetComponent<SpriteRenderer>().sprite;
        Init();
    }

    public void Init() {
        if (active)
            GetComponent<SpriteRenderer>().sprite = off;
        else
            GetComponent<SpriteRenderer>().sprite = off_inActive;
    }

    public void SetActive()
    {
        active = true;
        GetComponent<SpriteRenderer>().sprite = off;
    }

    public void Grey(bool value) {
        if (value) {
            active = false;
            GetComponent<SpriteRenderer>().sprite = off_inActive;
        } else {
            active = true;
            GetComponent<SpriteRenderer>().sprite = off;
        }
    }

    private void OnMouseDown() {

        audio.PlaySound(audio.buttonDown);
        pressActive = true;

        if (active)
        {
            GetComponent<SpriteRenderer>().sprite = on;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = on_inActive;
        }

        
    }

    private void OnMouseUp() {
        if (active)
        {
            GetComponent<SpriteRenderer>().sprite = off;
            if (pressActive)
                audio.PlaySound(audio.buttonUp);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = off_inActive;
            audio.PlaySound(audio.error);
        }

        if (pressActive)
            Actions.onButtonPressed(function);

        pressActive = false;
    }

    private void OnMouseExit() {
        

        if (active)
            GetComponent<SpriteRenderer>().sprite = off;
        else
            GetComponent<SpriteRenderer>().sprite = off_inActive;

        if (pressActive)
            audio.PlaySound(audio.buttonUp_grey);

        pressActive = false;
    }
}