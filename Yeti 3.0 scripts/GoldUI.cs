using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    public GameObject goldUI, goldUI_target, goldYeti, goldTimer, goldMultiplierText, timerScalar, floatingGoldText, floatingGoldText_trigger;
    public float time;
    float yScale, xScale;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = goldUI.transform.position;
        yScale = goldUI.transform.localScale.y;
        xScale = goldUI.transform.localScale.x;
        //goldUI.transform.localScale = new Vector3(xScale, 0);
    }

    public void TriggerGoldUI(int multiplier)
    {
        goldMultiplierText.GetComponent<TextMeshPro>().text = "X" + multiplier.ToString();
        MoveIn();
        Invoke("MoveOut", 1.5f);
        
    }

    public void CloseGoldUI()
    {
        goldTimer.SetActive(false);
        goldYeti.SetActive(false);
        timerScalar.transform.localScale = new Vector3(0, timerScalar.transform.localScale.y, 1);
        goldUI.transform.position = originalPos;
    }

    public void CreateGoldFloatingText()
    {
        GameObject floatingGoldText_inst = Instantiate(floatingGoldText, floatingGoldText_trigger.transform.position, Quaternion.identity);
    }

    public void MoveIn()
    {
        iTween.MoveTo(goldUI, goldUI_target.transform.position, time);
        //iTween.ScaleTo(goldUI, new Vector3(xScale, yScale), time);
    }

    public void MoveOut()
    {

        goldYeti.SetActive(true);
        goldTimer.SetActive(true);
        timerScalar.GetComponent<GoldTimer>().timerOn = true;
        iTween.MoveTo(goldUI, new Vector3(-2.018711f, -9.3f, -35.7f), time);

        //iTween.ScaleTo(goldUI, new Vector3(xScale, 0), time);
    }
}
