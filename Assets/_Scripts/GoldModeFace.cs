using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModeFace : GameElement
{
    float step;

    private void Start() {
        step = 0.0009f;
    }

    private void OnMouseDown() {
        game.controller.GoldMode_Transition();
        Destroy(gameObject);
    }

    private void Update() {
        transform.position = new Vector3(gameObject.transform.position.x + step, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
