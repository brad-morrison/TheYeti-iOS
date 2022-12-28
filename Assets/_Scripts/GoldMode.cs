using UnityEngine;

public class GoldMode : GameElement {

    // variables
    public bool goldMode;
    public float goldModeLength;
    public int goldModeMultiplier;
    // gameobjects & prefabs
    public GameObject goldMode_announceUI;
    public GameObject goldFlames, goldModeFace;
    
    public void GoldModeAnnounce() {
        goldMode_announceUI.SetActive(true);
        game.model.lifebar.animate = false;
    }

    public void ActivateGoldMode() {
        // hide announce ui
        goldMode_announceUI.SetActive(false);
        // restart lifebar animation
        game.model.lifebar.animate = true;
        // activate gold mode flag
        game.goldMode.goldMode = true;
        // activate gold mode for lifebar
        game.model.lifebar.GoldMode();
        // roll and set gold multiplier
        goldModeMultiplier = GoldMultiplierRoll();
        // turn on flames
        GoldFlames(true);
        // turn on outline
        game.yeti.yeti_goldOutline.SetActive(true);
    }

    public void DeactivateGoldMode() {
        // deactivate gold mode flag
        game.goldMode.goldMode = false;
        // activate normal mode for lifebar
        game.model.lifebar.NormalMode();
        // turn off flames
        GoldFlames(false);
        // turn off yeti outline
        game.yeti.yeti_goldOutline.SetActive(false);
    }

    public int GoldMultiplierRoll() {
        System.Random rand = new System.Random();
        int roll = rand.Next(1,5);
        Debug.Log("gold multiplier = " + roll);
        return roll;
    }

    public void GoldFlames(bool state) {
        foreach(Transform flame in game.goldMode.goldFlames.transform)
        {
            flame.GetComponent<SpriteRenderer>().enabled = state;
        }
    }
}