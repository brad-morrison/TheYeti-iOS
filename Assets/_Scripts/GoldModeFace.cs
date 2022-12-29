using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModeFace : MonoBehaviour
{
    float step;
    AudioSource source;
    public GameManager manager;

    private void Start() {
        source = GetComponent<AudioSource>();
        source.Play();
        step = 0.0009f;
    }

    private void OnMouseDown() {
        manager.ActivateGoldMode();
        Destroy(gameObject);
    }

    private void Update() {
        transform.position = new Vector3(gameObject.transform.position.x + step, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
