using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioSpriteController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerInputController movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerInputController>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        run.enabled = movement.running;

        if(movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if(movement.sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if(!movement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }
}
