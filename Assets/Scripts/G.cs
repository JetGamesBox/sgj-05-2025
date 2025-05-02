
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class G
{
    private static SceneController currentScene;
    public static InputController input = new InputController();
    public static SettingsController settings = new SettingsController();

    public static void OnSceneAwake(SceneController scene)
    {
        currentScene = scene;

        settings.ApplyAudioSettings(currentScene.audioMixer);
    }

    public static void Update()
    {
        input.Update();
    }

    public static void SwitchScene(Scenes scene)
    {
        currentScene.Disappear(() => { SceneManager.LoadScene(scene.ToString()); });
    }
}
