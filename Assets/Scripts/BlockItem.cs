using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        EntityMovement movement = GetComponent<EntityMovement>();

        rigidbody.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;

        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(.25f);

        spriteRenderer.enabled = true;

        float elasped = 0;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while(elasped < duration)
        {
            float timePercentage = elasped / duration;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, timePercentage);
            elasped += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition;

        rigidbody.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}
