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
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    public void DisableAnimations() {
        this.gameObject.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
