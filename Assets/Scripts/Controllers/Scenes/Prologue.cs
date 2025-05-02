using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : SceneController
{
    void Start()
    {
        G.SwitchScene(Scenes.Level1);
    }

    public override void Appear(Action callBack)
    {
                
    }

    private IEnumerator AppearProcess(Action callBack)
    {
        return null;
    }
}
