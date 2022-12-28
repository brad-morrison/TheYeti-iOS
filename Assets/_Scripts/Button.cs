using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : GameElement {
    public Sprite on, on_inActive, off_inActive;
    Sprite off;
    public string function;
    public bool pressActive;
    public bool active; // greyed out or not

    private void Start() {
        off = GetComponent<SpriteRenderer>().sprite;
    }

    public void SetActive()
    {
        active = true;
        GetComponent<SpriteRenderer>().sprite = off;
    }

    private void OnMouseDown() {
        if (active)
            GetComponent<SpriteRenderer>().sprite = on;
        else
            GetComponent<SpriteRenderer>().sprite = on_inActive;

        pressActive = true;
    }

    private void OnMouseUp() {
        if (active)
            GetComponent<SpriteRenderer>().sprite = off;
        else
            GetComponent<SpriteRenderer>().sprite = off_inActive;

        if (pressActive)
            game.input.ButtonPress(function);
    }

    private void OnMouseExit() {
        if (active)
            GetComponent<SpriteRenderer>().sprite = off;
        else
            GetComponent<SpriteRenderer>().sprite = off_inActive;

        pressActive = false;
    }
}