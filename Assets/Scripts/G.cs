
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class G
{
    private static SceneController currentScene;

    public static void Init(SceneController scene)
    {
        currentScene = scene;
    }

    public static void SwitchScene(Scenes scene)
    {
        currentScene.Disappear(() => { SceneManager.LoadScene(scene.ToString()); });
    }
}
