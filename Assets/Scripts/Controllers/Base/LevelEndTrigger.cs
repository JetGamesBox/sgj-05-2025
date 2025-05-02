using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sceneController.OnLevelCompleteTrigger();
    }
}
