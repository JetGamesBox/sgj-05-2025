using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tile : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;
    [SerializeField] public int index = -1;

    private Level2Controller sceneController;

    private SpriteRenderer tileSpriteRenderer;
    private Color tileStartColor;

    private Animator itemAnimator;

    private bool activated = false;

    private void Awake()
    {
        tileSpriteRenderer = GetComponent<SpriteRenderer>();
        tileStartColor = tileSpriteRenderer.color;

        Transform item = transform.Find("Item");

        if (item != null )
        {
            itemAnimator = item.GetComponent<Animator>();
            item.GetComponent<SpriteRenderer>().sprite = itemSprite;
        }
    }

    private void Start()
    {
        sceneController = (Level2Controller)G.currentScene;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        sceneController.OnTileEnter(collision, index, out activated);

        if (activated)
        {
            tileSpriteRenderer.color = tileStartColor * 0.7f;
            itemAnimator.SetBool("Activated", activated);
        }
    }

    public void Reset()
    {
        activated = false;
        itemAnimator.SetBool("Activated", activated);
        tileSpriteRenderer.color = tileStartColor;
    }
}
