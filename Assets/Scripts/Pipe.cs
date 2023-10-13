using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    //Variable for required key input.
    public KeyCode enterKeyCode = KeyCode.S;

    //Variables for Pipe Connection and Destination.
    public Vector3 enterDirection = Vector3.down; // this can vary depending on which direction the pipe is travelling, it is defaulted to down.
    public Vector3 exitDirection = Vector3.zero; // This will control which direction Mario exits a pipe. in the case where he simply spawns in place the default vector 3 (0,0,0) will be used.
    public Transform connection; // this will hold the co-ordinates of the end/exit pipe.

    //On Trigger Stay is used to detect a collision with the player and will continue to trigger as long as Mario remains in the trigger zone.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player")) // If the connection transform is not empty and the object interacting with the collider is the player, then excute the following logic:
        {
            if(Input.GetKey(enterKeyCode)) // if the required key stroke is entered, excute the following logic:
            {
                StartCoroutine(EnterPipe(other.transform)); // Start the courtine named EnterPipe (see below) and pass the transform of the player.
            }
        }
    }

    // This function is responsible for disabling the players main movement script and for determining the variables of the transition such as the where the player will transistion to and how much they will shrink during the animation.
    private IEnumerator EnterPipe(Transform player) 
    {
        player.GetComponent<PlayerInputController>().enabled = false; // Before we can animate the transition we must first disable the main player input script.

        Vector3 transitionPosition = transform.position + enterDirection; //Creates a vector3 from the pipes position to a unit in the direction of enterDirection 
        Vector3 transitionScale = Vector3.one * 0.5f; //this variable will be used to scale the player down by half to ensure it does not clip through the pipe anywhere.

        yield return Move(player, transitionPosition, transitionScale);
        yield return new WaitForSeconds(1f);

        bool underground = connection.position.y < 0f;
        Camera.main.GetComponent<SideScolling>().SetUnderground(underground);
        if(exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<PlayerInputController>().enabled = true;
        
    }

    // This Co-routine is responsible for moving the player sprite through the animation/transition.
    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f; // the amount of time elapsed since the co-routine was called
        float duration = 1f; // the length of time it should take the player to complete the transition

        Vector3 startPosition = player.position; //the players position at the start of the transition.
        Vector3 startScale = player.localScale; // the players scale at the start of the transtion.

        while (elapsed < duration) //While the time elapsed is less than the duration of the transition, excute the following logic:
        {
            float timePercentage = elapsed / duration; // a variable to hold the percentage of time remaining until the duration is fulfilled

            player.position = Vector3.Lerp(startPosition, endPosition, timePercentage);
            player.localScale = Vector3.Lerp(startScale, endScale, timePercentage);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;

    }
}
