using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : SceneController
{
    private void Start()
    {
        G.ShowSceneDialog(DialogPersones.Cat, "<����� ������� ������ � ������� ����>");
    }

    public override void OnSceneEvent(string eventName)
    {
        G.SwitchScene(Scenes.MainMenu);
    }
}
