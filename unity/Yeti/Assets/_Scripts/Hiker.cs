using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiker : MonoBehaviour
{
    public bool left;
    public bool frenzyTagged;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator Die() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void DisableAnimations() {
        animator.enabled = false;
    }
}
