using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueButton : MonoBehaviour
{
    [SerializeField] Prologue sceneController;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        sceneController = (Prologue) G.currentScene;
    }

    public void OnMouseEnter()
    {
        animator.SetBool("Hover", true);
    }

    public void OnMouseExit()
    {
        animator.SetBool("Hover", false);
    }
    public void OnMouseDown()
    {
        sceneController.OnSceneEvent("YesClick");
    }
}
