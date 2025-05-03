using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class Level3Controller : SceneController
{
    [SerializeField] private Transform queen;
    [SerializeField] private Transform gamingTable;

    protected override void Awake()
    {
        gamingTable.gameObject.SetActive(false);
        
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(CutSceneLevelBegin());
    }

    private IEnumerator CutSceneLevelBegin()
    {
        G.input.Blocked = true;

        G.ShowSceneDialog(DialogPersones.Cat, "������� ��������� ���������!$$$������� ����� ������ ���...", 2f);

        yield return new WaitForSeconds(5f);

        G.CameraFocus(queen.transform);
        G.ShowSceneDialog(DialogPersones.Queen, "����� ������, ���������!", 2.5f);

        yield return new WaitForSeconds(2.5f);

        G.ShowSceneDialog(DialogPersones.Cat, "���?", 1f);

        yield return new WaitForSeconds(1f);

        G.CameraFocus(player.transform);
        G.ShowSceneDialog(DialogPersones.Alice, "�����! �� ����?", 1.5f);

        yield return new WaitForSeconds(1.5f);

        G.CameraFocus(queen.transform);
        G.ShowSceneDialog(DialogPersones.Queen, "� ��, �������, �������!$$$������� � \"��������-����\"!", 2f);

        player.ForceVelocity(Vector2.up);

        yield return new WaitForSeconds(2f);

        G.ShowSceneDialog(DialogPersones.Queen, "���������� ���, ��� ������ ������� ��� ����� � ����.", 3f);

        yield return new WaitForSeconds(3f);

        gamingTable.gameObject.SetActive(true);

        G.CameraFocus(gamingTable.transform, 8.3f);
        G.ShowSceneDialog(DialogPersones.Queen, "���� �����!", 10f);
    }

    public override void OnSceneEvent(string eventName)
    {
        switch (eventName)
        {
            case "QueenTableTrigger": player.ForceVelocity(Vector2.zero); break;
        }
    }
}
