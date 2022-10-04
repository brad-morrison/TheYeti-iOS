using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed;
    public string direction;

    private void Start() {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (direction == "left" || direction == "down")
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x - step, transform.localPosition.y, transform.localPosition.z);
        }

        if (direction == "up" || direction == "right")
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x + step, transform.localPosition.y, transform.localPosition.z);
        }
        
    }
}
