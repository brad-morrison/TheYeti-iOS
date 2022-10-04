using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YetiSprite : MonoBehaviour
{
    public SpriteRenderer yetiRenderer;

    public Sprite left, right, bothUp, bothDown_1, bothDown_2, death;

    private void Awake() {
        yetiRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if (SceneManager.GetActiveScene().name != "game")
            StartCoroutine(Idle(0.5f));
    }
    public void ChangeLeft()
    {
        StartCoroutine(SwitchSprite(left, 0.1f));
    }

    public void ChangeRight()
    {
        StartCoroutine(SwitchSprite(right, 0.1f));
    }

    public void Idle1()
    {
        yetiRenderer.sprite = bothDown_1;
    }

    public void Idle2()
    {
        yetiRenderer.sprite = bothDown_2;
    }

    IEnumerator SwitchSprite(Sprite newSprite, float wait)
    {
        Sprite curSprite = yetiRenderer.sprite;
        yetiRenderer.sprite = newSprite;
        yield return new WaitForSeconds(wait);
        yetiRenderer.sprite = curSprite;
        yield return null;
    }

    IEnumerator Idle(float wait)
    {
        while (true)
        {
            Idle1();
            yield return new WaitForSeconds(wait);
            Idle2();
            yield return new WaitForSeconds(wait);
        }
    }
}
