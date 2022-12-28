using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameElement : MonoBehaviour
{
    public Game game { get { return GameObject.FindObjectOfType<Game>(); } }
}

public class Game : MonoBehaviour
{
    // MODEL      - all data and general variables for the game
    // CONTROLLER - main game logic, control and flow
    // SCRIPTS    - a GameObject that holds other scripts, individual scripts
    //              are grabbed at Awake() for easy access within other scripts.
    //              (hidden from inspector to look tidier).

    public GameModel model;
    public GameController controller;
    public GameView view;
    public GameObject scripts;

    // scripts
    //[HideInInspector]
    public Hikers hikers;
    //[HideInInspector]
    public Yeti yeti;

    void Awake()
    {
        Application.targetFrameRate = 600; // run at 60fps

        // script references
        hikers = scripts.GetComponent<Hikers>();
        yeti = scripts.GetComponent<Yeti>();
    }
}
