using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3DeckController : MonoBehaviour
{
    [SerializeField] Level3Controller sceneController;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        sceneController = (Level3Controller)G.currentScene;
    }

    private void OnMouseOver()
    {
        if (sceneController.CardWaiting())
            OnMouseEnter();
        else
            OnMouseExit();
    }

    public void OnMouseEnter()
    {
        animator.SetBool("Hover", true);
    }

    public void OnMouseExit()
    {
        animator.SetBool("Hover", false);
    }

    public void OnMouseUp()
    {
        sceneController.OnSceneEvent("DeckCardTrigger");
    }
}
