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
    public GameObject scripts;

    // scripts
    //[HideInInspector]
    public Costumes costumes;
    public Hikers hikers;
    public Yeti yeti;
    public GoldMode goldMode;
    public GameOver gameOver;
    public Audio audio;
    public UserInput input;
    public Particles particles;
    public MainMenu mainMenu;
    

    void Awake()
    {
        Application.targetFrameRate = 600; // run at 60fps

        // script references
        costumes = scripts.GetComponent<Costumes>();
        hikers = scripts.GetComponent<Hikers>();
        yeti = scripts.GetComponent<Yeti>();
        goldMode = scripts.GetComponent<GoldMode>();
        gameOver = scripts.GetComponent<GameOver>();
        audio = scripts.GetComponent<Audio>();
        input = scripts.GetComponent<UserInput>();
        particles = scripts.GetComponent<Particles>();
        mainMenu = scripts.GetComponent<MainMenu>();
    }
}
