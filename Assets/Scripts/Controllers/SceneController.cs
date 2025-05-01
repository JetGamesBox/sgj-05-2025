using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class SceneController : MonoBehaviour
{
    protected virtual void Awake()
    {
        G.Init(this);
    }

    public virtual void Disappear(Action callBack)
    {
        callBack.Invoke();
    }



}
