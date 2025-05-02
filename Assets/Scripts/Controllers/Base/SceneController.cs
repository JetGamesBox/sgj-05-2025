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

        Transform vc = transform.Find("VirtualCamera");

        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (player != null )
            vc.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("WorldBoundary"))
        {
            PolygonCollider2D worldBoundary = go.GetComponent<PolygonCollider2D>();
            vc.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = worldBoundary;
            break;
        }
        
        G.OnSceneAwake(this);
    }

    protected virtual void Update()
    {
        G.Update();
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
