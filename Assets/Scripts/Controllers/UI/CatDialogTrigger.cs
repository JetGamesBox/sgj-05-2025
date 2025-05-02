using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDialogTrigger : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private float delay = 3f;
    [SerializeField] private bool showOnce = true;

    private bool showed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!showed)
        {
            G.ShowSceneDialog(message, delay);
            showed = showOnce;
        }
    }
}
