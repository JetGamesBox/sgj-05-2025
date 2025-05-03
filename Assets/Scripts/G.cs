using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

using Transform = UnityEngine.Transform;

public static class G
{
    public static SceneController currentScene { get; private set; }
    private static CinemachineVirtualCamera currentCamera;

    public static InputController input = new InputController();
    public static SettingsController settings = new SettingsController();

    public static void OnSceneAwake(SceneController scene, Transform focus = null)
    {
        currentScene = scene;
        settings.ApplyAudioSettings(currentScene.audioMixer);

        Transform vc = scene.transform.Find("VirtualCamera");
        currentCamera = vc?.GetComponent<CinemachineVirtualCamera>();

        CameraFocus(focus);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("WorldBoundary"))
        {
            PolygonCollider2D worldBoundary = go.GetComponent<PolygonCollider2D>();
            vc.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = worldBoundary;
            break;
        }

        currentScene.Appear();
    }

    public static void CameraFocus(Transform focus, float zoom = 5f)
    {
        if (currentCamera != null && focus != null)
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
        currentScene?.Disappear(() => { SceneManager.LoadScene(scene.ToString()); });
    }

    public static void ShowSceneDialog(DialogPersones who, string message, float delay = 3f)
    {
        currentScene.interactiveDialog.Show(who, message, delay);
    }
}
