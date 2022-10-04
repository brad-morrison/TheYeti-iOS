using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldMultiplierText : MonoBehaviour
{
    GameController gameController;
    TextMeshPro textMesh;
    Color mainColor;
    public Color startColor;
    public float colorFade, floatTime;
    // Start is called before the first frame update
    
    private void Awake() {
        gameController = GameObject.Find("scripts").GetComponent<GameController>();
        textMesh = GetComponent<TextMeshPro>();
        textMesh.text = "X" + gameController.goldMultiplier.ToString();
    }
    void Start()
    {
        mainColor = textMesh.color;
        MoveText();
        Destroy(gameObject, 0.6f);
    }

    public void MoveText()
    {
        iTween.MoveBy(gameObject, new Vector3(0, 1.5f, 0), floatTime);
    }
    
    // Update is called once per frame
    void Update()
    {
        float step = (colorFade * Time.time);
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, step);
    }

}
