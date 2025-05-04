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

        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "��, ��� ��� �?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "�����, �� ��� �� ����� ���������� �� �������������� �������?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "�����? ��� �?", 1.5f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "�� � ��������! � ��� ������ ������ �� ���������!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "��-��-��... ��� �� ��� ������?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "���� ����� ��� ��������, ����� �������� �� ���������.");

        G.CameraFocus(focusPointBegin);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "������ ��������� � ����� ����� ���������. �����!");

        G.CameraFocus(player.transform);

        G.input.Blocked = false;
    }

    private IEnumerator CutSceneWormDialog()
    {
        G.input.Blocked = true;

        G.CameraFocus(worm);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Worm, "��... ���... �����?", 2f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.Worm, "�����? �����!", 2f);

        completeTrigger.gameObject.SetActive(true);
        G.CameraFocus(completeTrigger);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "�������, ������ � ���������!");

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
