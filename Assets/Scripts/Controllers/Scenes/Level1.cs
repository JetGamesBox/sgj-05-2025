using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : SceneController
{

    public override void OnLevelCompleteTrigger()
    {
        G.SwitchScene(Scenes.Level2);
    }

}
