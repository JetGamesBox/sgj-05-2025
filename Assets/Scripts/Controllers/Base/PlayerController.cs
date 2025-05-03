using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool collision = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        collision = true;
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        collision = false;
    }

    void Update()
    {
        Vector2 newVelocity = G.input.movementVector * speed;

        if (!Vector2.Equals(newVelocity, rb.velocity))
        {
            bool running = newVelocity.x != 0f || newVelocity.y != 0f;
            bool toLeft = rb.velocity.x < 0f;

            if (newVelocity.x == 0f && running && !collision)
                spriteRenderer.flipX = !spriteRenderer.flipX;
            else if (running)
                spriteRenderer.flipX = newVelocity.x < 0f;
            else
                spriteRenderer.flipX = toLeft;

            animator.SetBool("Running", running);
            rb.velocity = newVelocity;
        }

        if (collision && rb.velocity.x == 0f && rb.velocity.y == 0f)
            animator.SetBool("Running", false);


    }
}
