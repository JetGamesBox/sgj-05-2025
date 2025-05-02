using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;
using UnityEngine.UI;

public class CatDialogController : MonoBehaviour
{
    private float messageDelay = 3f;
    
    private Queue<string> messageQueue;
    private Text textbox;
    private Animator animator;

    private void Awake()
    {
        gameObject.SetActive(true);
        textbox = transform.Find("Text").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void Show(string strings, float delay = 3f)
    {
        StopAllCoroutines();
        messageDelay = delay;
        messageQueue = new Queue<string>(strings.Split("$$$"));
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        textbox.text = messageQueue.Dequeue();

        gameObject.SetActive(true);
        animator.SetBool("Show", true);
        yield return new WaitForSeconds(1f);

        while (messageQueue.Count > 0)
        {
            yield return new WaitForSeconds(messageDelay);
            textbox.text = messageQueue.Dequeue();
        }

        yield return new WaitForSeconds(messageDelay);

        animator.SetBool("Show", false);
        yield return new WaitForSeconds(1f);
    }
}
