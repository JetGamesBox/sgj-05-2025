using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectMoveDetector : MonoBehaviour
{
    [SerializeField] private Transform target;

    public bool Moving = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Moving && collision.transform == target)
            Moving = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Moving && collision.transform == target)
            Moving = false;
    }
}
