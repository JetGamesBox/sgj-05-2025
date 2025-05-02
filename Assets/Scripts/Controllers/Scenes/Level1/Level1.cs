using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : SceneController
{

    public void OnLevelComplete()
    {
        G.SwitchScene(Scenes.Level2);
    }

}
