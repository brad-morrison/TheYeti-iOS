using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float speed;
    float yKill = -10.5f;
    // Start is called before the first frame update
    void Start()
    {
        speed = SetSpeed();
        GetComponent<Collider2D>().enabled = RollCollider();
    }

    public float SetSpeed()
    {
        int flip = Random.Range(0,3);

        if (flip == 1)
            return Random.Range(200,400); // turn right
        else
            return Random.Range(-200, -400); // turn left
    }

    public bool RollCollider()
    {
        int roll = Random.Range(0, 4);

        if (roll == 1)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.time;
        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + step);

        if (yKill > transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
