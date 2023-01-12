using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.SceneManagement;

// base class (inherit from this to access the GameMaster)
// creates or gets the singleton 'GM' instance
// singleton instance name = project name as it's the highest level of control
// this derives from Monobehaviour so anything inheriting from this class also does
public class TheYeti : MonoBehaviour
{
    public GameMaster GM { get { return GameObject.FindObjectOfType<GameMaster>(); } }
}

// singleton class
public class GameMaster : MonoBehaviour
{
    public Audio audio;
    public Buttons buttons;
    public PlayerData playerData;
    public Leaderboards leaderboards;
    //
    public MainMenu mainMenu;
    public GameManager gameManager;
    public CostumeManager costumeManager;

    void Start()
    {
        // refs
        audio = GetComponentInChildren<Audio>();
        buttons = GetComponentInChildren<Buttons>();
        playerData = GetComponentInChildren<PlayerData>();
        leaderboards = GetComponentInChildren<Leaderboards>();
        // scene refs


        SceneChanged();

        DontDestroyOnLoad(this);

    }

    public void SceneChanged()
    {
        Debug.Log("new scene loaded");

        // refresh  connections
        if (GameObject.Find("GameManager"))
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (GameObject.Find("MainMenu"))
            mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();

        if (GameObject.Find("CostumeManager"))
            costumeManager = GameObject.Find("CostumeManager").GetComponent<CostumeManager>();
    }
}