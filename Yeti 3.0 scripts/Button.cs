using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    ButtonManager buttonManager;

    public Sprite on, on_inActive, off_inActive; // greyed out versions of sprites
    Sprite off; // references for control
    public bool active = true; // if greyed out or not
    bool pressActive = false; // active when finger is pressing

    private void Awake() {
        buttonManager = GameObject.Find("scripts").GetComponent<ButtonManager>();
        off = GetComponent<SpriteRenderer>().sprite;

    }

    public void SetInActive()
    {
        active = false;
        GetComponent<SpriteRenderer>().sprite = off_inActive;
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
            buttonManager.ButtonPress(gameObject.name);
    }

    private void OnMouseExit() {
        if (active)
            GetComponent<SpriteRenderer>().sprite = off;
        else
            GetComponent<SpriteRenderer>().sprite = off_inActive;

        pressActive = false;
    }
}
