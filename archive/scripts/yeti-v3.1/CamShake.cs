using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public float xShake, yShake, speed;
    
    public void Shake()
    {
        Vector3 shakeVector = new Vector3(xShake, yShake, 0);
        iTween.ShakePosition(this.gameObject, shakeVector, speed);
    }
}
