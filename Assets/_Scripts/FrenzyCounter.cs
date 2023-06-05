using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the Frenzy Mode counter interface
// 

public class FrenzyCounter : TheYeti
{
    public GameObject face1, face2, face3;

    private void Start()
    {
        ActivateFaces();
        StartCoroutine(DestroyAfter());
    }

    public void ActivateFaces()
    {
        if (GM.gameManager.frenzyMode.frenzyTokenCount == 1)
        {
            StartCoroutine(ActivateFaceAfter(face1));
        }

        if (GM.gameManager.frenzyMode.frenzyTokenCount == 2)
        {
            face1.SetActive(true);
            StartCoroutine(ActivateFaceAfter(face2));
        }

        if (GM.gameManager.frenzyMode.frenzyTokenCount == 3)
        {
            face1.SetActive(true);
            face2.SetActive(true);
            StartCoroutine(ActivateFaceAfter(face3));
        }
    }

    public IEnumerator ActivateFaceAfter(GameObject face)
    {
        yield return new WaitForSeconds(0.5f);
        GM.audio.PlaySound(GM.audio.frenzyCounter);
        face.SetActive(true);

        if (GM.gameManager.frenzyMode.frenzyTokenCount == 3 && !GM.gameManager.goldMode.goldMode)
        {
            GM.gameManager.frenzyMode.StartFrenzyTransition();
        }
    }

    public IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
