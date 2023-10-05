using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MarioSpriteController smallRenderer;
    public MarioSpriteController bigRenderer;
    private MarioSpriteController activeRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Hit()
    {
        if (big)
        {
            Shrink();
        }
        else
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DeathTrigger"))
        {
            Death();
        }
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, .5f);

        activeRenderer = bigRenderer;

        StartCoroutine(Scale());
    }

    private void Shrink()
    {
        bigRenderer.enabled = false;
        smallRenderer.enabled = true;

        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, .0f);

        StartCoroutine(Scale());
    }

    private IEnumerator Scale()
    {
        float elapsed = 0f;
        float duration = .5f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
}
