using UnityEngine;
using TMPro;

public class GoldMode : TheYeti {
    // variables
    public bool goldMode;
    public float goldModeLength;
    public int goldModeMultiplier;
    // gameobjects & prefabs
    public GameObject goldMode_announceUI;
    public GameObject goldFlames, goldModeFace, multiplierAnnounce, multiplierAnnounce_shadow, multiplierPop;
    private TextMeshPro multiplierText;
    private TextMeshPro multiplierShadowText;
    private SpriteRenderer[] goldFlameRenderers;

    private void Awake()
    {
        multiplierText = multiplierAnnounce.GetComponent<TextMeshPro>();
        multiplierShadowText = multiplierAnnounce_shadow.GetComponent<TextMeshPro>();
        goldFlameRenderers = goldFlames.GetComponentsInChildren<SpriteRenderer>();
    }

    public void GoldModeAnnounce() {
        // roll and set gold multiplier
        goldModeMultiplier = GoldMultiplierRoll();
        // set multiplier text
        string multiplierLabel = "x" + goldModeMultiplier.ToString();
        multiplierText.text = multiplierLabel;
        multiplierShadowText.text = multiplierLabel;

        goldMode_announceUI.SetActive(true);
        GM.gameManager.lifebar.animate = false;
        GM.audio.PlaySound(GM.audio.goldModeStart);
        // turn off input
        GM.gameManager.allowInput = false;
        // sky
        GM.gameManager.sky.GoldModeSky(true);
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
        // sky
        GM.gameManager.sky.GoldModeSky(false);
    }

    public int GoldMultiplierRoll() {
        int roll = Random.Range(2,5);
        Debug.Log("gold multiplier = " + roll);
        return roll;
    }

    public void GoldFlames(bool state) {
        foreach(SpriteRenderer flameRenderer in goldFlameRenderers)
        {
            flameRenderer.enabled = state;
        }
    }
}
