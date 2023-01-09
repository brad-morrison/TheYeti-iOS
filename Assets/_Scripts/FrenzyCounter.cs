using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzyCounter : MonoBehaviour
{
    public GameObject face1, face2, face3;
    public MasterManager master;

    private void Start()
    {
        master = GameObject.Find("MASTER_MANAGER").GetComponent<MasterManager>();
        ActivateFaces();
        StartCoroutine(DestroyAfter());
    }

    public void ActivateFaces()
    {
        if (master.gameManager.frenzyMode.frenzyTokenCount == 1)
        {
            StartCoroutine(ActivateFaceAfter(face1));
        }

        if (master.gameManager.frenzyMode.frenzyTokenCount == 2)
        {
            face1.SetActive(true);
            StartCoroutine(ActivateFaceAfter(face2));
        }

        if (master.gameManager.frenzyMode.frenzyTokenCount == 3)
        {
            face1.SetActive(true);
            face2.SetActive(true);
            StartCoroutine(ActivateFaceAfter(face3));
        }
    }

    public IEnumerator ActivateFaceAfter(GameObject face)
    {
        yield return new WaitForSeconds(0.5f);
        master.audio.PlaySound(master.audio.frenzyCounter);
        face.SetActive(true);

        if (master.gameManager.frenzyMode.frenzyTokenCount == 3 && !master.gameManager.goldMode.goldMode)
        {
            master.gameManager.frenzyMode.StartFrenzyTransition();
        }
    }

    public IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
