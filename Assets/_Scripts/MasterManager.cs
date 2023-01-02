using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
    public Audio audio;
    public Buttons buttons;
	public PlayerData playerData;
	//
	public GameManager gameManager;
	public MainMenu mainMenu;
	public CostumeManager costumeManager;

	void Start()
	{
		audio = GetComponentInChildren<Audio>();
		buttons = GetComponentInChildren<Buttons>();
		playerData = GetComponentInChildren<PlayerData>();

		SceneChanged();

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

