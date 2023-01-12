using UnityEngine;
using TMPro;

public class GoldMode : TheYeti {
    // variables
    public bool goldMode;
    public float goldModeLength;
    public int goldModeMultiplier;
    // gameobjects & prefabs
    public GameObject goldMode_announceUI;
    public GameObject goldFlames, goldModeFace, multiplierAnnounce, multiplierPop;

    public void GoldModeAnnounce() {
        // roll and set gold multiplier
        goldModeMultiplier = GoldMultiplierRoll();
        // set multiplier text
        multiplierAnnounce.GetComponent<TextMeshPro>().text = "x" + goldModeMultiplier.ToString();
        
        goldMode_announceUI.SetActive(true);
        GM.gameManager.lifebar.animate = false;
        GM.audio.PlaySound(GM.audio.goldModeStart);
        // turn off input
        GM.gameManager.allowInput = false;
    }

    public void ActivateGoldMode() {
        // hide announce ui
        goldMode_announceUI.SetActive(false);
        // turn on input
        GM.gameManager.allowInput = true;
        // restart lifebar animation
        GM.gameManager.lifebar.animate = true;
        // activate gold mode flag
        goldMode = true;
        // activate gold mode for lifebar
        GM.gameManager.lifebar.GoldMode();
        // turn on flames
        GoldFlames(true);
        // turn on outline
        GM.gameManager.yeti.yeti_goldOutline.SetActive(true);
    }

    public void DeactivateGoldMode() {
        // deactivate gold mode flag
        goldMode = false;
        // activate normal mode for lifebar
        GM.gameManager.lifebar.NormalMode();
        // turn off flames
        GoldFlames(false);
        // turn off yeti outline
        GM.gameManager.yeti.yeti_goldOutline.SetActive(false);
        GM.audio.PlaySound(GM.audio.goldModeEnd);
        // calc next goldmode spawn
        GM.gameManager.CalculateNextGoldModeSpawn();
    }

    public int GoldMultiplierRoll() {
        int roll = Random.Range(2,5);
        Debug.Log("gold multiplier = " + roll);
        return roll;
    }

    public void GoldFlames(bool state) {
        foreach(Transform flame in GM.gameManager.goldMode.goldFlames.transform)
        {
            flame.GetComponent<SpriteRenderer>().enabled = state;
        }
    }
}