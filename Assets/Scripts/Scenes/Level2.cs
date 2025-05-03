using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : SceneController
{
    [SerializeField] private Transform cup;
    [SerializeField] private Transform tiles;
    [SerializeField] private Transform hatMaster;
    [SerializeField] private Transform levelCompleteTrigger;
    [SerializeField] private Transform wallBehind;

    private Vector2 startPosition;
    private bool testInProgress = false;
    private bool testInPause = false;
    private bool testComplete = false;

    private List<Level2Tile> tilesList = new List<Level2Tile>();
    private int targetOrder = -1;
    private int currentOrder = -1;

    protected override void Awake()
    {
        startPosition = cup.transform.position;

        cup.gameObject.SetActive(false);
        levelCompleteTrigger.gameObject.SetActive(false);

        for (int i = 0;  i < tiles.childCount; i++)
        {
            Level2Tile child = tiles.GetChild(i).GetComponent<Level2Tile>();

            if (child == null)
                continue;

            child.ResetChecked(true);

            tilesList.Add(child);

            if (child.index >= 0)
                targetOrder++;
        }

        wallBehind.gameObject.SetActive(false);

        base.Awake();
    }

    private void Start()
    {
        //StartCoroutine(CutSceneBegin());
    }

    private IEnumerator CutSceneBegin()
    {

        G.input.Blocked = true;

        G.ShowSceneDialog(DialogPersones.Cat, "������, �� ����� �����!$$$ ������ ������ ���������. �� ��� ������� �������� ���...$$$�� �������... �� � ����", 2.5f);

        yield return new WaitForSeconds(8f);

        G.ShowSceneDialog(DialogPersones.Alice, "� ��� ��� ����� �������?", 2f);

        yield return new WaitForSeconds(2f);

        G.ShowSceneDialog(DialogPersones.Cat, "����� ��� ������ ������ ��������. �� ����������!", 3f);

        yield return new WaitForSeconds(3f);

        G.input.Blocked = false;

    }

    private IEnumerator CutSceneTestBegin()
    {
        testInProgress = true;
        testInPause = false;

        G.input.Blocked = true;

        wallBehind.gameObject.SetActive(true);

        G.CameraFocus(hatMaster.transform);

        G.ShowSceneDialog(DialogPersones.HatMaster, "� �� ��� ��� �����?$$$������� �������, �������� ��� ���!", 2f);

        yield return new WaitForSeconds(5f);

        G.ShowSceneDialog(DialogPersones.Cat, "� �� �������, ��� �� �� � ����...", 2f);

        yield return new WaitForSeconds(2f);

        foreach (Level2Tile tile in tilesList)
            tile.ResetChecked(false);

        G.CameraFocus(tiles.transform, 10.5f);

        G.ShowSceneDialog(DialogPersones.Cat, "������ ��� ��� �� ��� �������, � �� ���� ���������.", 4f);

        yield return new WaitForSeconds(1.5f);

        cup.gameObject.SetActive(true);
        G.CameraFocus(cup.transform);

        yield return new WaitForSeconds(1.5f);

        G.CameraFocus(tiles.transform, 10.5f);
        G.input.Blocked = false;

        HatMasterItemDialog();
    }

    private IEnumerator CutSceneTestEnd()
    {
        testComplete = true;
        testInProgress = false;
        testInPause = false;

        G.input.Blocked = true;

        G.CameraFocus(hatMaster.transform);
        G.ShowSceneDialog(DialogPersones.HatMaster, "���, ��� �����! ������ �����������?$$$���? ��, �����!", 2f);

        yield return new WaitForSeconds(5f);

        foreach (Level2Tile tile in tilesList)
            tile.ResetChecked(true);

        G.CameraFocus(player.transform);

        G.ShowSceneDialog(DialogPersones.Cat, "�����, �� ����������!", 2f);

        yield return new WaitForSeconds(2f);

        levelCompleteTrigger.gameObject.SetActive(true);
        G.CameraFocus(levelCompleteTrigger.transform);

        G.ShowSceneDialog(DialogPersones.Cat, "������ ��������� ������!");

        yield return new WaitForSeconds(2f);

        cup.gameObject.SetActive(false);
        G.CameraFocus(player.transform);

        G.input.Blocked = false;
    }

    private IEnumerator CutSceneTestReset()
    {
        G.input.Blocked = true;
        testInPause = true;

        G.CameraFocus(hatMaster.transform);

        G.ShowSceneDialog(DialogPersones.HatMaster, "���-���-���! ��� �� ���! ������ ��� ������ ��������!", 1.5f);
        yield return new WaitForSeconds(2f);

        G.CameraFocus(tiles.transform, 10.5f);
        HatMasterItemDialog();

        testInPause = false;
        G.input.Blocked = false;
    }

    private IEnumerator CutSceneTestResetAlt()
    {
        G.input.Blocked = true;
        testInPause = true;

        G.CameraFocus(hatMaster.transform);

        G.ShowSceneDialog(DialogPersones.HatMaster, "�� ������ ������, ��� ������� ���� ���������?!$$$...$$$����������� � ������ ���������� ���!", 4f);
        yield return new WaitForSeconds(13f);

        G.CameraFocus(tiles.transform, 10.5f);
        HatMasterItemDialog();

        testInPause = false;
        G.input.Blocked = false;
    }

    private void HatMasterItemDialog()
    {
        string text = "";

        switch (currentOrder + 1)
        {
            case 0:
            text = "������ ����� � ������������!";
            break;
            case 1:
            text = "������ � ���������!";
            break;
            case 2:
            text = "������ �������������! ������ ����� ����!";
            break;
            case 3:
            text = "�������-�����! �������-�������, ������!";
            break;
            case 4:
            text = "������ ����� � ������������!";
            break;
            case 5:
            text = "������ �������������! ������ ����� ����!";
            break;
            case 6:
            text = "������ ����� � ������������!";
            break;
            case 7:
            text = "�������-�����! �������-�������, ������!";
            break;
            case 8:
            text = "������ � ���������!";
            break;
            case 9:
            text = "������ ����� � ������������!";
            break;
            case 10:
            text = "������ �������������! ������ ����� ����!";
            break;
            default:
            return;
        }

        G.ShowSceneDialog(DialogPersones.HatMaster, text, 10f);
    }

    private void ResetTest(bool alt)
    {
        if (!testInProgress || testComplete || testInPause)
            return;

        StopAllCoroutines();

        currentOrder = -1;
        cup.transform.position = startPosition;

        foreach (Level2Tile tile in tilesList)
            tile.ResetChecked();

        if (alt)
            StartCoroutine(CutSceneTestResetAlt());
        else
            StartCoroutine(CutSceneTestReset());
    }

    public void OnTestTile()
    {
        if (testInProgress || testComplete)
            return;

        StartCoroutine(CutSceneTestBegin());
    }

    public void OnTileEnter(Collider2D collision, int index, out bool activated)
    {
        activated = false;

        if (collision.transform != cup.transform)
            return;

        if (!testInProgress || testInPause)
            return;

        currentOrder++;

        if (currentOrder != targetOrder && index == targetOrder)
            ResetTest(true);
        if (currentOrder != index)
            ResetTest(false);
        else if (currentOrder == targetOrder && index == targetOrder)
        {
            activated = true;
            StartCoroutine(CutSceneTestEnd());
        }
        else
        {
            activated = true;
            HatMasterItemDialog();
        }
    }

    public override void OnSceneEvent(string eventName)
    {
        switch (eventName)
        {
            case "TestBegin": StartCoroutine(CutSceneTestBegin()); break;
            case "LevelComplete": G.SwitchScene(Scenes.Level3); break;
        }            
    }
}
