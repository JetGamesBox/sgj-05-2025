using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogPersones who = DialogPersones.Cat;
    [SerializeField] private string message;
    [SerializeField] private float delay = 3f;
    [SerializeField] private bool showOnce = true;

    private bool showed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!showed)
        {
            G.ShowSceneDialog(who, message, delay);
            showed = showOnce;
        }
    }
}
