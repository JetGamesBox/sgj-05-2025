using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Level1Controller : SceneController
{
    [SerializeField] private Transform worm;
    [SerializeField] private Transform focusPointBegin;
    [SerializeField] private Transform completeTrigger;

    protected override void Awake()
    {
        base.Awake();

        completeTrigger.gameObject.SetActive(false);

        player.GetComponent<Light2D>().enabled = true;
    }

    private void Start()
    {
        StartCoroutine(CutSceneLevelBegin());
    }

    private IEnumerator CutSceneLevelBegin()
    {
        G.input.Blocked = true;

        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Ой, где это я?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Алиса, ну что ты опять переходишь по подозрительным ссылкам?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Чешир? Где я?", 1.5f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Ты в даркнете! И так просто отсюда не выбраться!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Ой-ёй-ёй... Что же мне делать?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Надо сжать твоё сознание, иначе файрволл не пропустит.");

        G.CameraFocus(focusPointBegin);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Первый архиватор в конце этого лабиринта. Вперёд!");

        G.CameraFocus(player.transform);

        G.input.Blocked = false;
    }

    private IEnumerator CutSceneWormDialog()
    {
        G.input.Blocked = true;

        G.CameraFocus(worm);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Worm, "Ты... кто... такая?", 2f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.Worm, "Глитч? Уходи!", 2f);

        completeTrigger.gameObject.SetActive(true);
        G.CameraFocus(completeTrigger);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Быстрее, прыгай в архиватор!");

        G.CameraFocus(player.transform);

        G.input.Blocked = false;
    }

    public override void OnSceneEvent(string eventName)
    {
        switch (eventName)
        {
            case "WormDialog": StartCoroutine(CutSceneWormDialog()); break;
            case "LevelComplete": G.SwitchScene(Scenes.Level2); break;
        }            
    }

}
