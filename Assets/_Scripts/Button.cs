using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {
    public GameManager manager;
    public Sprite on, on_inActive, off_inActive;
    Sprite off;
    public string function;
    public bool pressActive;
    public bool active; // greyed out or not
    public UserInput userInput;

    private void Awake() {
        userInput = GameObject.Find("UserInput").GetComponent<UserInput>();
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
            userInput.ButtonPress(function);
    }

    private void OnMouseExit() {
        if (active)
            GetComponent<SpriteRenderer>().sprite = off;
        else
            GetComponent<SpriteRenderer>().sprite = off_inActive;

        pressActive = false;
    }
}