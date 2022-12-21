using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiker : MonoBehaviour
{
    //public bool dead;
    public bool left;

    // Start is called before the first frame update
    void Start()
    {
        //dead = false;
    }

    public IEnumerator Die() {
        Debug.Log("running");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
