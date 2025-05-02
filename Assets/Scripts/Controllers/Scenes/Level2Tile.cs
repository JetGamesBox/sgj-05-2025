using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tile : MonoBehaviour
{
    [SerializeField] Level2Controller sceneController;
    [SerializeField] private int index = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sceneController.OnTileEnter(collision, index);
    }
}
