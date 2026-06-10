using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Button : MonoBehaviour {
    public Audio audio;
    public Sprite on, on_inActive, off_inActive;
    Sprite off;
    SpriteRenderer spriteRenderer;
    public string function;
    public bool pressActive;
    public bool active; // greyed out or not

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject audioObject = GameObject.Find("Audio");
        if (audioObject != null)
            audio = audioObject.GetComponent<Audio>();

        off = spriteRenderer.sprite;
        Init();
    }

    public void Init() {
        if (active)
            spriteRenderer.sprite = off;
        else
            spriteRenderer.sprite = off_inActive;
    }

    public void SetActive()
    {
        active = true;
        spriteRenderer.sprite = off;
    }

    public void Grey(bool value) {
        if (value) {
            active = false;
            spriteRenderer.sprite = off_inActive;
        } else {
            active = true;
            spriteRenderer.sprite = off;
        }
    }

    private void OnMouseDown() {

        if (audio != null)
            audio.PlaySound(audio.buttonDown);
        pressActive = true;

        if (active)
        {
            spriteRenderer.sprite = on;
        }
        else
        {
            spriteRenderer.sprite = on_inActive;
        }

        
    }

    private void OnMouseUp() {
        if (active)
        {
            spriteRenderer.sprite = off;
            if (pressActive && audio != null)
                audio.PlaySound(audio.buttonUp);
        }
        else
        {
            spriteRenderer.sprite = off_inActive;
            if (audio != null)
                audio.PlaySound(audio.error);
        }

        if (pressActive && active)
            Actions.ButtonPressed(function);

        pressActive = false;
    }

    private void OnMouseExit() {
        

        if (active)
            spriteRenderer.sprite = off;
        else
            spriteRenderer.sprite = off_inActive;

        if (pressActive && audio != null)
            audio.PlaySound(audio.buttonUp_grey);

        pressActive = false;
    }
}
