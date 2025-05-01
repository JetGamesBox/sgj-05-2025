using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator?.SetBool("Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator?.SetBool("Hover", false);
    }
}
