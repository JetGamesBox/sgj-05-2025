using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BackgroundParlax : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float parlaxStrenght = 0.1f;

    private void Update()
    {
        transform.position = -player.position * parlaxStrenght;
    }
}
