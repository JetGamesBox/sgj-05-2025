using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tile : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;
    [SerializeField] public int index = -1;
    [SerializeField] private FMODUnity.EventReference itemPickSound;

    private Level2Controller sceneController;

    private SpriteRenderer tileSpriteRenderer;
    private Color tileStartColor;

    private Animator itemAnimator;

    private bool @checked = false;

    private void Awake()
    {
        tileSpriteRenderer = GetComponent<SpriteRenderer>();
        tileStartColor = tileSpriteRenderer.color;

        Transform item = transform.Find("Item");

        if (item != null )
        {
            itemAnimator = item.GetComponent<Animator>();
            item.GetComponent<SpriteRenderer>().sprite = itemSprite;
            itemAnimator.SetFloat("SpeedAnimation", 1f + Random.Range(-0.2f, 0.2f));
        }
    }

    private void PlaySound()
    {
        if (itemPickSound.IsNull)
            return;

        FMODUnity.RuntimeManager.PlayOneShot(itemPickSound);
    }

    private void Start()
    {
        sceneController = (Level2Controller) G.currentScene;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (@checked)
            return;

        sceneController.OnTileEnter(collision, index, out @checked);

        if (@checked)
        {
            tileSpriteRenderer.color = tileStartColor * 0.7f;
            itemAnimator.SetBool("Activated", @checked);
            PlaySound();
        }
    }

    public void ResetChecked(bool value = false)
    {
        @checked = value;
        itemAnimator?.SetBool("Activated", @checked);

        if (tileSpriteRenderer != null)
            tileSpriteRenderer.color = tileStartColor;
    }
}
