using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private bool once = true;

    private bool triggered = false;
    
    private void Triger()
    {
        if (!triggered)
        {
            sceneController.OnLevelCompleteTrigger();
            triggered = once;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Triger();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Triger();
    }
}
