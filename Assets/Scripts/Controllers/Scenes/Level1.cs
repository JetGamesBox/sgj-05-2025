using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : SceneController
{
    protected override void Awake()
    {
        base.Awake();

        G.CameraFocus(player.transform, 5f);
    }

    public override void OnLevelCompleteTrigger()
    {
        G.SwitchScene(Scenes.Level2);
    }

}
