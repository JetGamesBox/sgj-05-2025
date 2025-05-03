using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    public Vector2 movementVector { get; private set; }

    public bool Blocked { get; set; }
    public InputController()
    {
        Blocked = false;
    }

    public void Update()
    {
        movementVector = Vector2.zero;

        if (Blocked)
            return;

        if (Input.GetKey(KeyCode.W))
            movementVector += Vector2.up;

        if (Input.GetKey(KeyCode.S))
            movementVector += Vector2.down;

        if (Input.GetKey(KeyCode.A))
            movementVector += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            movementVector += Vector2.right;

        movementVector.Normalize();
    }
}
