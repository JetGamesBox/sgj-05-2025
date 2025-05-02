using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2TestTrigger : MonoBehaviour
{
    [SerializeField] private Level2Controller sceneController;

    private void OnTriggerStay2D(Collider2D collision)
    {
        sceneController.OnTestTile();
    }
}
