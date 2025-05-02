using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class InteractiveDialogController : MonoBehaviour
{
    [SerializeField] private Sprite iconAlice;
    [SerializeField] private Color colorAlice;
    [SerializeField] private Sprite iconCat;
    [SerializeField] private Color colorCat;
    [SerializeField] private Sprite iconWorm;
    [SerializeField] private Color colorWorm;
    [SerializeField] private Sprite iconHatMaster;
    [SerializeField] private Color colorHatMaster;
    [SerializeField] private Sprite iconQueen;
    [SerializeField] private Color colorQueen;

    private Image icon;
    private Image border;
    private Text textbox;
    private Animator animator;

    private float messageDelay = 3f;
    private Dictionary<DialogPersones, Persone> persones = new Dictionary<DialogPersones, Persone>();
    private Queue<string> messageQueue;

    private void Awake()
    {
        persones.Add(DialogPersones.Alice, new Persone(iconAlice, colorAlice));
        persones.Add(DialogPersones.Cat, new Persone(iconCat, colorCat));
        persones.Add(DialogPersones.Worm, new Persone(iconWorm, colorWorm));
        persones.Add(DialogPersones.HatMaster, new Persone(iconHatMaster, colorHatMaster));
        persones.Add(DialogPersones.Queen, new Persone(iconQueen, colorQueen));

        icon = transform.Find("Icon")?.GetComponent<Image>();
        border = transform.Find("Border")?.GetComponent<Image>();

        gameObject.SetActive(true);
        textbox = transform.Find("Text").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void Show(DialogPersones who, string strings, float delay = 3f)
    {
        StopAllCoroutines();

        Persone p = persones[who];

        icon.sprite = p.icon;
        border.color = p.color;
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
