using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveDialogController : MonoBehaviour
{
    [SerializeField] private List<Persone> persones = new List<Persone>();

    private UnityEngine.UI.Image icon;
    private UnityEngine.UI.Image border;
    private UnityEngine.UI.Image background;
    private Text textbox;
    private Animator animator;

    private float messageDelay = 3f;

    private Dictionary<DialogPersones, Persone> personesList = new Dictionary<DialogPersones, Persone>();
    private Queue<string> messageQueue;

    private FMODUnity.EventReference currentVoiceEvent;

    private void Awake()
    {
        foreach (Persone pers in persones)
            personesList.Add(pers.who, pers);

        icon = transform.Find("Icon")?.GetComponent<UnityEngine.UI.Image>();
        border = transform.Find("Border")?.GetComponent<UnityEngine.UI.Image>();
        background = transform.Find("Background")?.GetComponent<UnityEngine.UI.Image>();

        gameObject.SetActive(true);
        textbox = transform.Find("Text").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void Show(DialogPersones who, string strings, float delay = 3f)
    {
        StopAllCoroutines();

        Persone p = personesList[who];

        currentVoiceEvent = p.voice;

        icon.sprite = p.icon;
        border.color = p.color;
        background.color = p.color;
        messageDelay = delay;
        messageQueue = new Queue<string>(strings.Split("$$$"));

        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        textbox.text = messageQueue.Dequeue();
        PlayVoice();

        gameObject.SetActive(true);
        animator.SetBool("Show", true);
        yield return new WaitForSeconds(1f);

        while (messageQueue.Count > 0)
        {
            yield return new WaitForSeconds(messageDelay);
            textbox.text = messageQueue.Dequeue();
            PlayVoice();
        }

        yield return new WaitForSeconds(messageDelay);

        animator.SetBool("Show", false);
        yield return new WaitForSeconds(1f);
    }

    private void PlayVoice()
    {
        if (currentVoiceEvent.IsNull)
            return;

        FMODUnity.RuntimeManager.PlayOneShot(currentVoiceEvent);
    }
}
