using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public bool rotate = false;
    private void Update() {
        if (rotate) {
            this.gameObject.transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
        }
    }
}