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

        G.ShowSceneDialog(DialogPersones.Cat, "Алиса, ну что ты опять переходишь по подозрительным ссылкам?$$$Ты в даркнете!", 2.5f);

        yield return new WaitForSeconds(6f);

        G.ShowSceneDialog(DialogPersones.Alice, "Кто ты? Что это за место?$$$И что мне теперь делать?", 2.5f);

        yield return new WaitForSeconds(6f);

        G.ShowSceneDialog(DialogPersones.Cat, "Чтобы выбраться отсюда, надо сжать сознание!$$$Иначе фаерволл не пропустит!", 2.5f);

        yield return new WaitForSeconds(6f);

        G.CameraFocus(focusPointBegin);
        G.ShowSceneDialog(DialogPersones.Cat, "Впереди лабиринт, и в конце первый архиватор!$$$Вперед!", 2f);

        yield return new WaitForSeconds(6f);

        G.CameraFocus(player.transform);

        G.input.Blocked = false;
    }

    private IEnumerator CutSceneWormDialog()
    {
        G.input.Blocked = true;

        G.CameraFocus(worm);
        G.ShowSceneDialog(DialogPersones.Worm, "Ты... кто... такая?$$$Глитч? Уходи!", 1f);

        yield return new WaitForSeconds(4f);

        completeTrigger.gameObject.SetActive(true);
        G.CameraFocus(completeTrigger);
        G.ShowSceneDialog(DialogPersones.Cat, "Быстрее, прыгай в архиватор!");

        yield return new WaitForSeconds(2f);

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
