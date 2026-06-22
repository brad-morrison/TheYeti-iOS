using System.Collections;
using UnityEngine;

public class ShieldFallEffect : MonoBehaviour
{
    public void Play(Animator animator, string animationStateName, float duration, float gravityScale, Vector2 velocity, float spin, float spinDelay, float animationStartTime, float animationDuration)
    {
        Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
        body.gravityScale = gravityScale;
        body.linearVelocity = velocity;
        body.angularVelocity = 0f;

        StartCoroutine(AnimateAndDestroy(animator, animationStateName, duration, body, spin, spinDelay, animationStartTime, animationDuration));
    }

    private IEnumerator AnimateAndDestroy(Animator animator, string animationStateName, float duration, Rigidbody2D body, float spin, float spinDelay, float animationStartTime, float animationDuration)
    {
        duration = Mathf.Max(0.01f, duration);
        spinDelay = Mathf.Max(0f, spinDelay);
        animationStartTime = Mathf.Clamp01(animationStartTime);
        animationDuration = Mathf.Max(0.01f, animationDuration);
        float elapsed = 0f;
        bool spinStarted = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            float animationProgress = Mathf.Clamp01(elapsed / animationDuration);

            if (!spinStarted && body != null && (elapsed >= spinDelay || body.linearVelocity.y <= 0f))
            {
                body.angularVelocity = spin;
                spinStarted = true;
            }

            if (animator != null)
            {
                float normalizedAnimationTime = Mathf.Lerp(animationStartTime, 0.999f, animationProgress);
                animator.Play(animationStateName, 0, normalizedAnimationTime);
                animator.Update(0f);
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
