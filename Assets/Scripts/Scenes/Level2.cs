using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : SceneController
{
    [SerializeField] private Collider2D movableObject;
    [SerializeField] private GameObject tiles;
    [SerializeField] private GameObject hatMaster;

    private Vector2 startPosition;
    private bool testInProgress = false;
    private bool testFinished = false;

    private int targetOrder = 10;
    private int currentOrder = -1;

    protected override void Awake()
    {
        startPosition = movableObject.transform.position;
        movableObject.gameObject.SetActive(false);

        base.Awake();
    }

    private IEnumerator CutSceneTestBegin()
    {
        G.input.Blocked = true;

        G.CameraFocus(hatMaster.transform);

        yield return new WaitForSeconds(2f);

        G.CameraFocus(tiles.transform, 10.5f);

        BeginTest();
        G.input.Blocked = false;
    }

    private IEnumerator CutSceneTestEnd()
    {
        G.input.Blocked = true;

        G.CameraFocus(hatMaster.transform);

        yield return new WaitForSeconds(2f);

        G.CameraFocus(player.transform);

        EndTest();

        G.input.Blocked = false;
    }

    private void BeginTest()
    {
        testInProgress = true;
        movableObject.gameObject.SetActive(true);
        G.ShowSceneDialog(DialogPersones.Cat, "Тест диалогового окна");
    }

    private void ResetTest()
    {
        if (!testInProgress || testFinished)
            return;

        currentOrder = -1;
        movableObject.transform.position = startPosition;
    }

    private void EndTest()
    {
        if (!testInProgress || testFinished)
            return;

        testFinished = true;
        testInProgress = false;

        movableObject.gameObject.SetActive(false);

        G.CameraFocus(player.transform);
    }

    public void OnTestTile()
    {
        if (testInProgress || testFinished)
            return;

        StartCoroutine(CutSceneTestBegin());
    }

    public void OnTileEnter(Collider2D collision, int index)
    {
        if (collision != movableObject)
            return;

        if (!testInProgress)
            return;

        if (++currentOrder != index)
            ResetTest();
        else if (index == targetOrder)
            StartCoroutine(CutSceneTestEnd());
    }

    public override void OnSceneEvent(string eventName)
    {
        if (testFinished)
            G.SwitchScene(Scenes.Level3);
    }
}
