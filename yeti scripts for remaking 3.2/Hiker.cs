using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiker : MonoBehaviour
{
    SpriteRenderer hikerRenderer;
    GameController gameController;
    public Sprite climb1, climb2, death;
    public string side;
    public string color;

    private void Awake() {
        gameController = GameObject.Find("scripts").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        hikerRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Idle(RandomFloatBetween(0.2f, 0.3f)));

        if(gameObject.transform.position.x < 1)
            side = "left";
        else
            side = "right";

        if(gameObject.name.Contains("Red"))
            color = "red";
        else
            color = "green";
    }

    public void MoveUp(GameObject target)
    {
        Vector3 newPos = new Vector3(gameObject.transform.position.x, target.transform.position.y, 1);

        iTween.MoveTo(gameObject, newPos, 0.3f);
    }

    float RandomFloatBetween(float min, float max)
    {
        return Random.Range(min, max);
    }

    IEnumerator Idle(float wait)
    {
        while (gameController.dead != true)
        {
            hikerRenderer.sprite = climb1;
            yield return new WaitForSeconds(wait);
            hikerRenderer.sprite = climb2;
            yield return new WaitForSeconds(wait);
        }
    }
}
