using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddCoin();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = .25f;

        while (elapsed < duration)
        {
            float timePercent = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, timePercent);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}