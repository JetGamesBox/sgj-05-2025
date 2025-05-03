using System.Collections;
using System.Collections.Generic;

using UnityEditor.U2D.Aseprite;

using UnityEngine;

public class Level2Controller : SceneController
{
    [SerializeField] private Collider2D cup;
    [SerializeField] private Transform tiles;
    [SerializeField] private Transform hatMaster;
    [SerializeField] private Transform levelCompleteTrigger;

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

            if (child != null && child.index >= 0)
                tilesList.Add(child);
        }

        targetOrder = tilesList.Count - 1;

        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(CutSceneBegin());
    }

    private IEnumerator CutSceneBegin()
    {

        G.input.Blocked = true;

        G.ShowSceneDialog(DialogPersones.Cat, "Хорошо, ты стала легче!$$$ Теперь второй архиватор. Он под охраной любителя чая...$$$Он немного... Не в себе", 2.5f);

        yield return new WaitForSeconds(8f);

        G.ShowSceneDialog(DialogPersones.Alice, "И что мне нужно сделать?", 2f);

        yield return new WaitForSeconds(2f);

        G.ShowSceneDialog(DialogPersones.Cat, "Давай для начала найдем Шляпника. Ты справишься!", 3f);

        yield return new WaitForSeconds(3f);

        G.input.Blocked = false;

    }

    private IEnumerator CutSceneTestBegin()
    {
        testInProgress = true;
        testInPause = false;

        G.input.Blocked = true;

        G.CameraFocus(hatMaster.transform);

        G.ShowSceneDialog(DialogPersones.HatMaster, "А ты кто еще такая?$$$Впрочем неважно, сделайка мне чаю!", 2f);

        yield return new WaitForSeconds(5f);

        G.ShowSceneDialog(DialogPersones.Cat, "Я же говорил, что он не в себе...", 2f);

        yield return new WaitForSeconds(2f);

        cup.gameObject.SetActive(true);

        G.CameraFocus(cup.transform);

        G.ShowSceneDialog(DialogPersones.Cat, "Сделай ему чай по его рецепту, и он тебя пропустит.", 2f);

        yield return new WaitForSeconds(4f);

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
        G.ShowSceneDialog(DialogPersones.HatMaster, "Ура, чай готов! Хочешь попробовать?$$$Нет? Ну, ладно!", 2f);

        yield return new WaitForSeconds(5f);

        G.ShowSceneDialog(DialogPersones.Cat, "Алиса, ты справилась!", 2f);

        yield return new WaitForSeconds(2f);

        levelCompleteTrigger.gameObject.SetActive(true);
        G.CameraFocus(levelCompleteTrigger.transform);

        G.ShowSceneDialog(DialogPersones.Cat, "Второй архиватор открыт!");

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

        G.ShowSceneDialog(DialogPersones.HatMaster, "Нет-нет-нет! Все не так! Теперь все заново начинать!", 1.5f);
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

        G.ShowSceneDialog(DialogPersones.HatMaster, "Ты правда думала, что сможешь меня обхитрить?!$$$...$$$Возвращайся и сделай правильный чай!", 4f);
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
            text = "Толкай чашку к бамбуршлягцу!";
            break;
            case 1:
            text = "Добавь эшпендреконта!";
            break;
            case 2:
            text = "Теперь промтер-чпобр!";
            break;
            case 3:
            text = "Толкай к шклягонту!";
            break;
            case 4:
            text = "Быстрее-быстрее, к бумкемону! Толкай!";
            break;
            case 5:
            text = "Немного щиплютора добавь!";
            break;
            case 6:
            text = "А теперь воблештуж! Толкай туда!";
            break;
            case 7:
            text = "Пумпиний! Да, пумпиний, двигай туда!";
            break;
            case 8:
            text = "На очереди дырджемак!";
            break;
            case 9:
            text = "Мое любимое - квашносоп, толкай туда!";
            break;
            case 10:
            text = "И финальный штрих... Мумумабрю!";
            break;
            default: return;
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
            tile.Reset();

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

    public void OnTileEnter(Collider2D collision, int index)
    {
        if (collision != cup)
            return;

        if (!testInProgress || testInPause)
            return;

        currentOrder++;

        if (currentOrder != targetOrder && index == targetOrder)
            ResetTest(true);
        if (currentOrder != index)
            ResetTest(false);
        else if (currentOrder == targetOrder && index == targetOrder)
            StartCoroutine(CutSceneTestEnd());
        else
            HatMasterItemDialog();
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
