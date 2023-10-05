using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public int maxHits = -1;
    public Sprite emptyBlock;

    private bool animating;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if(collision.transform.DotTest(transform, Vector2.up))
            {
                HitBlock();
            }
        }
    }

    private void HitBlock()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if(item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * .5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = .125f;

        while(elapsed < duration)
        {
            float timePercent = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, timePercent);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
