using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tile : MonoBehaviour
{
    [SerializeField] Level2Controller sceneController;
    [SerializeField] public int index = -1;

    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private bool activated = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        sceneController.OnTileEnter(collision, index, out activated);

        if (activated)
            spriteRenderer.color = startColor * 0.7f;
    }

    public void Reset()
    {
        activated = false;
        spriteRenderer.color = startColor;
    }
}
