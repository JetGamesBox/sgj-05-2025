using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1EndTrigger : MonoBehaviour
{
    [SerializeField] private Level1Controller sceneController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sceneController.OnLevelComplete();
    }
}
