using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : SceneController
{
    void Start()
    {
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(5f);

        G.SwitchScene(Scenes.Level1);
    }
}
