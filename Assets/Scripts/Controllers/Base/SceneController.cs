using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Audio;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public AudioMixer audioMixer;

    protected virtual void Awake()
    {
        audioMixer = GetComponent<AudioMixer>();

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
