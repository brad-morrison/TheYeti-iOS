using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModeFace : MonoBehaviour
{
    float step;
    AudioSource audioSource;
    public MasterManager manager;

    private void Start() {
        manager = GameObject.Find("MASTER_MANAGER").GetComponent<MasterManager>();
        audioSource = GetComponent<AudioSource>();

        if (!manager.playerData.GetSfx()) // debug
            audioSource.mute = true;

        step = 0.0009f;
    }

    private void OnMouseDown() {
        manager.gameManager.ActivateGoldMode();
        Destroy(gameObject);
    }

    private void Update() {
        transform.position = new Vector3(gameObject.transform.position.x + step, gameObject.transform.position.y, gameObject.transform.position.z);
        if (transform.position.x > 1.5f)
        {
            manager.gameManager.CalculateNextGoldModeSpawn();
            Destroy(gameObject);
        }
            
    }

    public IEnumerator KillAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
