using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModeFace : MonoBehaviour
{
    float step;
    AudioSource audioSource;
    public GameManager manager;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        step = 0.0009f;
        StartCoroutine(KillAfter(5));
    }

    private void OnMouseDown() {
        manager.ActivateGoldMode();
        Destroy(gameObject);
    }

    private void Update() {
        transform.position = new Vector3(gameObject.transform.position.x + step, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    public IEnumerator KillAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
