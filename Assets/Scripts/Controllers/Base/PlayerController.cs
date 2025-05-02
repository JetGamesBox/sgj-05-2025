using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 newVelocity = G.input.movementVector * speed;

        if (!Vector2.Equals(newVelocity, rb.velocity))
            rb.velocity = newVelocity;
    }
}
