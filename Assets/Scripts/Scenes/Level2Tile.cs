using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tile : MonoBehaviour
{
    [SerializeField] Level2Controller sceneController;
    [SerializeField] public int index = -1;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        activated = true;
        sceneController.OnTileEnter(collision, index);
    }

    public void Reset()
    {
        activated = false;
    }
}
