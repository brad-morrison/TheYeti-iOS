using System.Collections;
using UnityEngine;

public enum HikerSide
{
    Left,
    Right
}

public class Hiker : MonoBehaviour
{
    public bool left;
    public bool frenzyTagged;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetSide(HikerSide side, float offsetX)
    {
        left = side == HikerSide.Left;
        spriteRenderer.flipX = left;

        if (left)
            animator.SetBool("Left", false);

        float direction = left ? 1 : -1;
        transform.position = new Vector2(transform.position.x + offsetX * direction, transform.position.y);
    }

    public void SetFrenzyTagged(bool value)
    {
        frenzyTagged = value;
        spriteRenderer.color = value ? Color.red : Color.white;
    }

    public void PlayDeath()
    {
        spriteRenderer.sortingOrder = 10;
        animator.SetBool("Dead", true);
        StartCoroutine(Die());
    }

    public IEnumerator Die() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void DisableAnimations() {
        animator.enabled = false;
    }
}
