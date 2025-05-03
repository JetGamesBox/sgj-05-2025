using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class InteractiveDialogController : MonoBehaviour
{
    [SerializeField] private List<Persone> persones = new List<Persone>();

    private Image icon;
    private Image border;
    private Text textbox;
    private Animator animator;

    private float messageDelay = 3f;

    private Dictionary<DialogPersones, Persone> personesList = new Dictionary<DialogPersones, Persone>();
    private Queue<string> messageQueue;

    private FMODUnity.EventReference currentSpeaker;

    private void Awake()
    {
        foreach (Persone pers in persones)
            personesList.Add(pers.who, pers);

        icon = transform.Find("Icon")?.GetComponent<Image>();
        border = transform.Find("Border")?.GetComponent<Image>();

        gameObject.SetActive(true);
        textbox = transform.Find("Text").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void Show(DialogPersones who, string strings, float delay = 3f)
    {
        StopAllCoroutines();

        Persone p = personesList[who];

        currentSpeaker = p.voice;

        icon.sprite = p.icon;
        border.color = p.color;
        messageDelay = delay;
        messageQueue = new Queue<string>(strings.Split("$$$"));

        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        textbox.text = messageQueue.Dequeue();
        FMODUnity.RuntimeManager.PlayOneShot(currentSpeaker);

        gameObject.SetActive(true);
        animator.SetBool("Show", true);
        yield return new WaitForSeconds(1f);

        while (messageQueue.Count > 0)
        {
            yield return new WaitForSeconds(messageDelay);
            textbox.text = messageQueue.Dequeue();
            FMODUnity.RuntimeManager.PlayOneShot(currentSpeaker);
        }

        yield return new WaitForSeconds(messageDelay);

        animator.SetBool("Show", false);
        yield return new WaitForSeconds(1f);
    }
}
