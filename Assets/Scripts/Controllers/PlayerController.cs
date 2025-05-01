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
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            direction += Vector2.up;

        if (Input.GetKey(KeyCode.S))
            direction += Vector2.down;

        if (Input.GetKey(KeyCode.A))
            direction += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            direction += Vector2.right;

        direction.Normalize();

        rb.velocity = direction * speed;


    }
}
