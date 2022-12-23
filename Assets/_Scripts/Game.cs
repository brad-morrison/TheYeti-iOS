using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameElement : MonoBehaviour
{
    public Game game { get { return GameObject.FindObjectOfType<Game>(); } }
}

public class Game : MonoBehaviour
{
    public GameModel model;
    public GameController controller;
    public GameView view;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 600; // 60fps
    }
}
