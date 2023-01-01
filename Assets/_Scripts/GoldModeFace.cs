using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModeFace : MonoBehaviour
{
    float step;
    AudioSource audioSource;
    public GameManager manager;

    private void Start() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        if (manager.sound) // debug
            audioSource.Play();
        step = 0.0009f;
        //StartCoroutine(KillAfter(10));
    }

    private void OnMouseDown() {
        manager.ActivateGoldMode();
        Destroy(gameObject);
    }

    private void Update() {
        transform.position = new Vector3(gameObject.transform.position.x + step, gameObject.transform.position.y, gameObject.transform.position.z);
        if (transform.position.x > 1.5f)
        {
            manager.CalculateNextGoldModeSpawn();
            Destroy(gameObject);
        }
            
    }

    public IEnumerator KillAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
