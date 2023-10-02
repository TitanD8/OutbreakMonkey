using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    private Rigidbody2D entityRigidbody;
    private Vector2 velocity;


    private void Awake()
    {
        entityRigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        entityRigidbody.WakeUp();
    }

    private void OnDisable()
    {
        entityRigidbody.velocity = Vector2.zero;
        entityRigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        entityRigidbody.MovePosition(entityRigidbody.position + velocity * Time.fixedDeltaTime);
     
        if(entityRigidbody.Raycast(direction))
        {
            direction = -direction;
        }

        if(entityRigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }    
    }

}
