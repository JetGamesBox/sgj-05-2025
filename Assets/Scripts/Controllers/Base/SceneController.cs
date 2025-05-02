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
        
        G.OnSceneAwake(this, player.transform);
    }

    protected virtual void Update()
    {
        G.Update();
    }

    public virtual void OnLevelCompleteTrigger()
    {
    }

    public virtual void Appear(Action callBack)
    {
        callBack.Invoke();
    }

    public virtual void Disappear(Action callBack)
    {
        callBack.Invoke();
    }
}
