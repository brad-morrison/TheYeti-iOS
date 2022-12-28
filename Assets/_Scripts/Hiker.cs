using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiker : MonoBehaviour
{
    public bool left;

    public IEnumerator Die() {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    public void DisableAnimations() {
        this.gameObject.GetComponent<Animator>().enabled = false;
    }
}
