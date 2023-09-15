using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    
    // Update is called once per frame
    void FixedUpdate()
    {
        // if the enemy's current destination is patrol point zero then move toward that point.
       if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);
            //if the enemy has reached its current destination, switch to patrol point one and flip sprite.
            if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < 0.2f)
            {
                transform.localScale = new Vector3(-0.8f, 3f, 1f);
                patrolDestination = 1;
            }
        }
        // if the enemy's current destination is patrol point one then move toward that point.
        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);
            //if the enemy has reached its current destination, switch to patrol point zero and flip sprite.
            if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < 0.2f)
            {
                transform.localScale = new Vector3(0.8f, 3f, 1f);
                patrolDestination = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit Player");
            Destroy(other.gameObject);
        }
    }
}
