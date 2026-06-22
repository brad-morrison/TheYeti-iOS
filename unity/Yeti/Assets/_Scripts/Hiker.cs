using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum HikerSide
{
    Left,
    Right
}

public enum ShieldHitResult
{
    None,
    Cracked,
    Broken
}

public class Hiker : MonoBehaviour
{
    private const string ShieldBreakStateName = "anim_shield_break";

    public bool left;
    public bool frenzyTagged;
    public bool shielded;

    [SerializeField] private string shieldChildName = "shield";
    [SerializeField] private float shieldBrokenNormalizedTime = 0.2f;
    [SerializeField] private float shieldShudderDuration = 0.22f;
    [SerializeField] private float shieldShudderStrength = 0.06f;
    [SerializeField] private int shieldSortingOrder = 12;
    [SerializeField] private float shieldFallDuration = 2f;
    [SerializeField] private float shieldBreakAnimationDuration = 0.35f;
    [SerializeField] private float shieldFallGravityScale = 1.35f;
    [SerializeField] private Vector2 shieldPopVelocity = new Vector2(0.35f, 1.2f);
    [SerializeField] private float shieldPopVelocityJitter = 0.12f;
    [SerializeField] private float shieldFallSpin = 95f;
    [SerializeField] private float shieldFallSpinDelay = 0.12f;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer shieldRenderer;
    private Animator animator;
    private Animator shieldAnimator;
    private int shieldHitCount;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        Transform shield = transform.Find(shieldChildName);
        if (shield != null)
        {
            shieldRenderer = shield.GetComponent<SpriteRenderer>();
            shieldAnimator = shield.GetComponent<Animator>();
        }

        SetShielded(false);
    }

    public void SetSide(HikerSide side, float offsetX)
    {
        left = side == HikerSide.Left;
        spriteRenderer.flipX = left;

        if (shieldRenderer != null)
            shieldRenderer.flipX = left;

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

    public void SetShielded(bool value)
    {
        shielded = value && shieldRenderer != null;
        shieldHitCount = 0;

        SetShieldVisible(shielded);

        if (shieldAnimator == null)
            return;

        shieldAnimator.enabled = shielded;
        shieldAnimator.speed = 0f;

        if (shielded)
        {
            shieldRenderer.sortingOrder = Mathf.Max(shieldRenderer.sortingOrder, spriteRenderer.sortingOrder + 1);
            shieldAnimator.Play(ShieldBreakStateName, 0, 0f);
            shieldAnimator.Update(0f);
        }
    }

    public ShieldHitResult HandleShieldHit()
    {
        if (!shielded)
            return ShieldHitResult.None;

        if (shieldHitCount == 0)
        {
            shieldHitCount++;
            ShowBrokenShield();
            ShudderShieldHit();
            return ShieldHitResult.Cracked;
        }

        DropShield();
        return ShieldHitResult.Broken;
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

        if (shieldAnimator != null)
            shieldAnimator.enabled = false;
    }

    private void DropShield()
    {
        shielded = false;
        shieldHitCount = 0;

        if (shieldRenderer == null)
            return;

        transform.DOComplete();

        Transform shield = shieldRenderer.transform;
        float horizontalDirection = left ? -1f : 1f;
        shieldRenderer.sortingOrder = shieldSortingOrder;

        if (shieldAnimator != null)
        {
            shieldAnimator.enabled = true;
            shieldAnimator.speed = 0f;
        }

        shield.SetParent(null, true);

        ShieldFallEffect fallEffect = shield.gameObject.AddComponent<ShieldFallEffect>();
        Vector2 popVelocity = new Vector2(
            shieldPopVelocity.x * horizontalDirection + Random.Range(-shieldPopVelocityJitter, shieldPopVelocityJitter),
            shieldPopVelocity.y + Random.Range(0f, shieldPopVelocityJitter)
        );

        fallEffect.Play(
            shieldAnimator,
            ShieldBreakStateName,
            shieldFallDuration,
            shieldFallGravityScale,
            popVelocity,
            shieldFallSpin * -horizontalDirection,
            shieldFallSpinDelay,
            shieldBrokenNormalizedTime,
            shieldBreakAnimationDuration
        );

        shieldRenderer = null;
        shieldAnimator = null;
    }

    private void ShowBrokenShield()
    {
        if (shieldAnimator == null)
            return;

        shieldAnimator.enabled = true;
        shieldAnimator.speed = 0f;
        shieldAnimator.Play(ShieldBreakStateName, 0, Mathf.Clamp01(shieldBrokenNormalizedTime));
        shieldAnimator.Update(0f);
    }

    private void ShudderShieldHit()
    {
        transform.DOComplete();
        transform.DOShakePosition(shieldShudderDuration, shieldShudderStrength, 18, 90f, false, true);
    }

    private void SetShieldVisible(bool visible)
    {
        if (shieldRenderer != null)
            shieldRenderer.enabled = visible;
    }
}
