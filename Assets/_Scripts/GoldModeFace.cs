using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModeFace : TheYeti
{
    public AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        if (!GM.playerData.GetSfx()) // debug
            audioSource.mute = true;
    }

    private void OnMouseDown() {
        GM.gameManager.ActivateGoldMode();
        Destroy(gameObject);
    }

    private void Update() {
        if (transform.position.x > 1.5f)
        {
            GM.gameManager.CalculateNextGoldModeSpawn();
            Destroy(gameObject);
        }

        if (GM.gameManager.gameOver.gameOver || GM.gameManager.frenzyMode.frenzyMode)
            Destroy(gameObject);

    }

    public IEnumerator KillAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
