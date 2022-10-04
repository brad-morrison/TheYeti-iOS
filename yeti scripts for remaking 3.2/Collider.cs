using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    GameController gameController;
    // Start is called before the first frame update
    void Awake()
    {
        gameController = GameObject.Find("scripts").GetComponent<GameController>();
    }

    private void OnMouseDown() {
        gameController.UserInput(gameObject.name);
    }
}
