using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float step;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x + step, transform.position.y);
        if (transform.position.x < -1.8f) transform.position = new Vector2(1.8f, transform.position.y);
    }
}