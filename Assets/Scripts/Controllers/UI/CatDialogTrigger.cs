using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDialogTrigger : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private float delay = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        G.ShowSceneDialog(message, delay);
    }
}
