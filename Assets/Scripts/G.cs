
using System;
using System.Collections;
using Cinemachine;

using UnityEngine;
using UnityEngine.SceneManagement;

using static UnityEngine.RuleTile.TilingRuleOutput;

using Transform = UnityEngine.Transform;

public static class G
{
    private static SceneController currentScene;
    private static CinemachineVirtualCamera currentCamera;

    public static InputController input = new InputController();
    public static SettingsController settings = new SettingsController();

    public static void OnSceneAwake(SceneController scene, Transform focus = null)
    {
        currentScene = scene;
        settings.ApplyAudioSettings(currentScene.audioMixer);

        Transform vc = scene.transform.Find("VirtualCamera");
        currentCamera = vc.GetComponent<CinemachineVirtualCamera>();

        CameraFocus(focus);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("WorldBoundary"))
        {
            PolygonCollider2D worldBoundary = go.GetComponent<PolygonCollider2D>();
            vc.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = worldBoundary;
            break;
        }

        currentScene.Appear(() => { G.input.Blocked = false; });
    }

    public static void CameraFocus(Transform focus, float zoom = 5f)
    {
        if (currentCamera != null)
        {
            currentCamera.Follow = focus;
            currentCamera.m_Lens.OrthographicSize = zoom;
        }
    }

    public static void Update()
    {
        input.Update();
    }

    public static void SwitchScene(Scenes scene)
    {
        G.input.Blocked = true;
        currentScene.Disappear(() => { SceneManager.LoadScene(scene.ToString()); });
    }
}
