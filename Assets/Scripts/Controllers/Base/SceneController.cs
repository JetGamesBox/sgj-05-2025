using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Audio;

public class SceneController : MonoBehaviour
{
    protected CatDialogController catDialog;
    [HideInInspector] public AudioMixer audioMixer;

    protected virtual void Awake()
    {
        catDialog = transform.Find("Interface")?.Find("CatDialogController")?.GetComponent<CatDialogController>();
        audioMixer = GetComponent<AudioMixer>();

        CinemachineVirtualCamera vc = transform.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (player != null )
            vc.Follow = player.transform;

        G.OnSceneAwake(this);
    }

    protected virtual void Update()
    {
        G.Update();
    }

    public virtual void Disappear(Action callBack)
    {
        callBack.Invoke();
    }
}
