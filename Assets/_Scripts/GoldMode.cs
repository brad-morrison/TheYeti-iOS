using UnityEngine;
using TMPro;

public class GoldMode : MonoBehaviour {
    public GameManager manager;
    // variables
    public bool goldMode;
    public float goldModeLength;
    public int goldModeMultiplier;
    // gameobjects & prefabs
    public GameObject goldMode_announceUI;
    public GameObject goldFlames, goldModeFace, multiplierAnnounce, multiplierPop;
    public Audio audio;

    private void Start()
    {
        audio = GameObject.Find("Audio").GetComponent<Audio>();
    }

    public void GoldModeAnnounce() {
        // roll and set gold multiplier
        goldModeMultiplier = GoldMultiplierRoll();
        // set multiplier text
        multiplierAnnounce.GetComponent<TextMeshPro>().text = "x" + goldModeMultiplier.ToString();
        
        goldMode_announceUI.SetActive(true);
        manager.lifebar.animate = false;
        audio.PlaySound(audio.goldModeStart);
        // turn off input
        manager.allowInput = false;
    }

    public void ActivateGoldMode() {
        // hide announce ui
        goldMode_announceUI.SetActive(false);
        // turn on input
        manager.allowInput = true;
        // restart lifebar animation
        manager.lifebar.animate = true;
        // activate gold mode flag
        goldMode = true;
        // activate gold mode for lifebar
        manager.lifebar.GoldMode();
        // turn on flames
        GoldFlames(true);
        // turn on outline
        manager.yeti.yeti_goldOutline.SetActive(true);
    }

    public void DeactivateGoldMode() {
        // deactivate gold mode flag
        goldMode = false;
        // activate normal mode for lifebar
        manager.lifebar.NormalMode();
        // turn off flames
        GoldFlames(false);
        // turn off yeti outline
        manager.yeti.yeti_goldOutline.SetActive(false);
        audio.PlaySound(audio.goldModeEnd);
        // calc next goldmode spawn
        manager.CalculateNextGoldModeSpawn();
    }

    public int GoldMultiplierRoll() {
        int roll = Random.Range(2,5);
        Debug.Log("gold multiplier = " + roll);
        return roll;
    }

    public void GoldFlames(bool state) {
        foreach(Transform flame in manager.goldMode.goldFlames.transform)
        {
            flame.GetComponent<SpriteRenderer>().enabled = state;
        }
    }
}