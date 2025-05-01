using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : SceneController
{
    void Start()
    {
        G.SwitchScene(Scenes.GamePlayTest);
    }
}
