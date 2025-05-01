using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSorting : MonoBehaviour
{
    [SerializeField] private bool updateInTime = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Objects";
        UpdateOrderLayer();
    }

    private void FixedUpdate()
    {
        if (updateInTime)
            UpdateOrderLayer();
    }

    private void UpdateOrderLayer()
    {
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = -(int)(transform.position.y * 10);
    }
}
