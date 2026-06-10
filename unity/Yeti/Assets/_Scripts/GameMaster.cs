using UnityEngine;
using UnityEngine.SceneManagement;

// base class (inherit from this to access the GameMaster)
// creates or gets the singleton 'GM' instance
// singleton instance name = project name as it's the highest level of control
// this derives from Monobehaviour so anything inheriting from this class also does
public class TheYeti : MonoBehaviour
{
    public GameMaster GM { get { return GameMaster.Instance; } }
}

// singleton class
public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    public static GameMaster Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameMaster>();

            return instance;
        }
    }

    public PlayerData playerData;
    public Audio audio;
    public Buttons buttons;
    public Leaderboards leaderboards;
    //
    public MainMenu mainMenu;
    public GameManager gameManager;
    public CostumeManager costumeManager;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        // refs
        playerData = GetComponentInChildren<PlayerData>();
        audio = GetComponentInChildren<Audio>();
        buttons = GetComponentInChildren<Buttons>();
        leaderboards = GetComponentInChildren<Leaderboards>();
        // scene refs

        SceneChanged();
    }

    void OnDestroy()
    {
        if (instance != this)
            return;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        instance = null;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneChanged();
    }

    public void SceneChanged()
    {
        Debug.Log("new scene loaded");

        // refresh  connections
        gameManager = FindObjectOfType<GameManager>();
        mainMenu = FindObjectOfType<MainMenu>();
        costumeManager = FindObjectOfType<CostumeManager>();
    }
}
