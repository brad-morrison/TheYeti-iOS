using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationOffset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomOffset;
        randomOffset = Random.Range(0f, 1f);
        GetComponent<Animator>().SetFloat("offset", randomOffset);
    }
}
