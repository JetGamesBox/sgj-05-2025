using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Audio;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public InteractiveDialogController interactiveDialog;
    [HideInInspector] public AudioMixer audioMixer;
    protected PlayerController player;

    protected virtual void Awake()
    {
        interactiveDialog = transform.Find("Interface")?.Find("DialogController")?.GetComponent<InteractiveDialogController>();
        audioMixer = GetComponent<AudioMixer>();

        player = FindAnyObjectByType<PlayerController>();

        if (player == null)
            G.OnSceneAwake(this);
        else
            G.OnSceneAwake(this, player.transform);
    }

    protected virtual void Update()
    {
        G.Update();
    }

    public virtual void OnSceneEvent(string eventName)
    {
    }

    public virtual void Appear()
    {
        G.input.Blocked = false;
    }

    public virtual void Disappear(Action callBack)
    {
        G.input.Blocked = true;
        callBack?.Invoke();
    }
}
