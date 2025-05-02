using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private SceneController sceneController;
    [SerializeField] private string eventName = "LevelComplete";
    [SerializeField] private bool once = true;

    private bool triggered = false;

    private void Awake()
    {
        sceneController = G.currentScene;
    }

    private void Triger()
    {
        if (!triggered)
        {
            sceneController.OnSceneEvent(eventName);
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
