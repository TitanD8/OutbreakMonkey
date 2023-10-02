using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (other.transform.DotTest(this.transform, Vector2.down))
            {
                Flatten();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
}