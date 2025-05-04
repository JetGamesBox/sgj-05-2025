using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

        player.GetComponent<Light2D>().enabled = true;
    }

    private void Start()
    {
        StartCoroutine(CutSceneBegin());
    }

    private IEnumerator CutSceneBegin()
    {

        G.input.Blocked = true;

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Хорошо, ты стала легче! Теперь второй архиватор.");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Его охраняет любитель чая. Он немного... Не в себе.");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Чешир, не знала, что ты тоже любишь канселить всех подряд?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Что? Нет, я не такой! Ты скоро сама всё поймешь.");

        G.input.Blocked = false;

    }

    private IEnumerator CutSceneTestBegin()
    {
        testInProgress = true;
        testInPause = false;

        G.input.Blocked = true;

        wallBehind.gameObject.SetActive(true);

        G.CameraFocus(hatMaster.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "О-хо-хо? Гость? В моём холостяцком жныжнике?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Где-где?", 1.5f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "Любишь чай? Я вот и дня не могу без крямкочки чая!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Чего-чего чая?", 1.5f);

        G.CameraFocus(player.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "А я говорил?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Просто сделай для него чай по его безумному рецепту.");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Но как я пойму, что добавлять?");

        foreach (Level2Tile tile in tilesList)
            tile.ResetChecked(false);

        cup.gameObject.SetActive(true);
        G.CameraFocus(cup.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Я верю в тебя, Алиса! Ты справишься!");

        G.CameraFocus(tiles.transform, 11.86f);

        HatMasterItemDialog();

        G.input.Blocked = false;
    }

    private IEnumerator CutSceneTestEnd()
    {
        testComplete = true;
        testInProgress = false;
        testInPause = false;

        G.input.Blocked = true;

        G.CameraFocus(hatMaster.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "Ура, чай готов!");

        foreach (Level2Tile tile in tilesList)
            tile.ResetChecked(true);

        G.CameraFocus(player.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Алиса, я в тебе не сомневался!");

        cup.gameObject.SetActive(false);
        G.CameraFocus(hatMaster.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "Как же он хорош! Чефирь, амбозия, каламбрамбрина!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "Хочешь попробовать? Нет? Ну, ладно!");

        levelCompleteTrigger.gameObject.SetActive(true);
        G.CameraFocus(levelCompleteTrigger.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Алиса, скорее в архиватор, пока он не допил чай!");

        G.CameraFocus(player.transform);

        G.input.Blocked = false;
    }

    private IEnumerator CutSceneTestReset()
    {
        G.input.Blocked = true;
        testInPause = true;

        G.CameraFocus(hatMaster.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "Нет-нет-нет, я же другое сказал! Заново!");

        G.CameraFocus(tiles.transform, 11.86f);
        HatMasterItemDialog();

        testInPause = false;
        G.input.Blocked = false;
    }

    private IEnumerator CutSceneTestResetAlt()
    {
        G.input.Blocked = true;
        testInPause = true;

        G.CameraFocus(hatMaster.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "А где же чай?", 4f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "В крямкочке совсем не чай!", 4f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.HatMaster, "Нет чая — нет хода дальше!", 4f);

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
            text = "Щепотку шклягонта!";
            break;
            case 2:
            text = "Добавь эшпендреконта! Толкай чашку туда!";
            break;
            case 3:
            text = "Промтер-чпобр! Быстрее-быстрее, толкай!";
            break;
            case 4:
            text = "Толкай чашку к бамбуршлягцу!";
            break;
            case 5:
            text = "Добавь эшпендреконта! Толкай чашку туда!";
            break;
            case 6:
            text = "Снова к бамбуршлягцу!";
            break;
            case 7:
            text = "Промтер-чпобр! Быстрее-быстрее, толкай!";
            break;
            case 8:
            text = "Еще немного шклягонта!";
            break;
            case 9:
            text = "Бамбуршлягц! Последний!";
            break;
            case 10:
            text = "И завершить все эшпендреконтом!";
            break;
            default:
            return;
        }

        G.ShowSceneDialog(DialogPersones.HatMaster, text, 30f);
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
