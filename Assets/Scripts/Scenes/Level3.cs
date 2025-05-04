using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FMODUnity;
using FMOD.Studio;

using UnityEngine;

public class Level3Controller: SceneController
{
    [SerializeField] private Transform queen;
    [SerializeField] private Transform gamingTable;
    [SerializeField] private Transform levelCompleteTrigger;
    [SerializeField] private Level3CardController cardPrefab;
    [SerializeField] private List<Sprite> cardSprites = new List<Sprite>();

    [SerializeField] private EventReference fmodParameter;
    [SerializeField] private string globalParameterName = "CardsIntensity";
    [SerializeField] private EventReference CardPick;
    [SerializeField] private EventReference CardRiff;

    private bool cardPicked = false;
    private bool cardWaiting = false;

    private List<Level3CardController> cardsList = new List<Level3CardController>();

    protected override void Awake()
    {
        levelCompleteTrigger.gameObject.SetActive(false);
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

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Остался последний архиватор!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Кажется здесь никого нет...");

        G.CameraFocus(queen.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Брысь отсюда, хвостатый!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Мяу?", 1f);

        G.CameraFocus(player.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Чешир! Ты куда?", 2f);

        G.CameraFocus(queen.transform);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "А ты, девочка, подойди!");

        player.ForceVelocity(Vector2.up);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Сыграем в \"двадцать-одно\"!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Выигрывает тот, кто первым соберет это число у себя.");

        gamingTable.gameObject.SetActive(true);
        G.CameraFocus(gamingTable.transform, 8.3f);

        PrepeareRound("Round1");
        RuntimeManager.PlayOneShot(CardRiff);

        for (int i = 0; i < 2; i++)
        {
            cardWaiting = true;

            yield return G.ShowSceneDialogUntil(DialogPersones.Queen, "Тяни карту!", CardPicked, 30f);

            cardsList[i * 2].MoveToPoint();
            RuntimeManager.PlayOneShot(CardPick);

            yield return new WaitForSeconds(2f);

            cardsList[i * 2 + 1].MoveToPoint();
            RuntimeManager.PlayOneShot(CardPick);
        }

        yield return new WaitForSeconds(2f);

        SetCardsIntensity(1);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "У меня 21!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Фи! Вообще-то в первом раунде надо было собрать 20!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Победа моя!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Но ведь...", 1f);
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Ты смеешь спорить с королевой?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Нет, но...", 1f);

        MoveCardsInDeck();

        RuntimeManager.PlayOneShot(CardRiff);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Вот и славно! Новый раунд!");


        ClearTable();


        PrepeareRound("Round2");


        for (int i = 0; i < 3; i++)
        {
            cardWaiting = true;

            yield return G.ShowSceneDialogUntil(DialogPersones.Queen, "Тяни карту!", CardPicked, 30f);

            cardsList[i * 2].MoveToPoint();
            RuntimeManager.PlayOneShot(CardPick);

            yield return new WaitForSeconds(2f);

            cardsList[i * 2 + 1].MoveToPoint();
            RuntimeManager.PlayOneShot(CardPick);
        }

        yield return new WaitForSeconds(2f);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "У меня снова 21!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Что это там у тебя? Королева черви? Глупости!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Королева может быть только одна!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Но ведь у вас тоже королева...");

        MoveCardsInDeck();

        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Глупости! Паршивые карты! Новый раунд!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Вы ведь опять правила поменяете?");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Глупости! Правило одно: Королева всегда права!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "...", 1f);

        RuntimeManager.PlayOneShot(CardRiff);

        SetCardsIntensity(2);

        ClearTable();

        PrepeareRound("Round3"); 


        for (int i = 0; i < 3; i++)
        {
            cardWaiting = true;

            yield return G.ShowSceneDialogUntil(DialogPersones.Queen, "Тяни карту!", CardPicked, 30f);

            cardsList[i * 2].MoveToPoint();
            RuntimeManager.PlayOneShot(CardPick);

            yield return new WaitForSeconds(2f);

            cardsList[i * 2 + 1].MoveToPoint();
            RuntimeManager.PlayOneShot(CardPick);
        }

        yield return new WaitForSeconds(2f);



        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "О, как мне везёт! Для победы надо собрать как раз 33!");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Queen, "Ах, девочка моя, сразу видно, что благородных кровей в тебе нет.");
        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Хсссс, ты кого в песочницу отправляешь, а? Королева жуликов!");

        gamingTable.gameObject.SetActive(false);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Иди сама в песочке копайся!");

        queen.gameObject.SetActive(false);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Alice, "Чешир! Ты вернулся!", 1.5f);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Потом поговорим, Алиса!", 2f);

        levelCompleteTrigger.gameObject.SetActive(true);
        G.CameraFocus(levelCompleteTrigger);

        yield return G.ShowSceneDialogAndWait(DialogPersones.Cat, "Беги к третьему архиватору!", 2f);

        gamingTable.gameObject.SetActive(false);

        G.CameraFocus(player.transform);

        G.input.Blocked = false;        

    }

    private void MoveCardsInDeck()
    {
        foreach (Level3CardController card in cardsList)
        {
            card.pointToMove = gamingTable;
            card.MoveToPoint();
        }

    }

    private void ClearTable()
    {
        foreach (Level3CardController card in cardsList)
            Destroy(card.gameObject);

        cardsList.Clear();
    }

    private void NewCard(Vector3 startPosition, Transform pointTomove, Sprite sprite)
    {
        Level3CardController card = Instantiate(cardPrefab, gamingTable.transform);
        //card.transform.position = startPosition;
        card.pointToMove = pointTomove;
        card.cardFace = sprite;

        cardsList.Add(card);


    }

    private Sprite CardSpriteByName(string name)
    {
        int index = 0;

        switch (name)
        {
            case "ThreeHearts":
            index = 1;
            break;
            case "FiveDiamonds":
            index = 2;
            break;
            case "SixClubs":
            index = 3;
            break;
            case "SevenSpides":
            index = 4;
            break;
            case "TenClubs":
            index = 5;
            break;
            case "EightHears":
            index = 6;
            break;
            case "NineSpides":
            index = 7;
            break;
            case "NineClubs":
            index = 8;
            break;
            case "AceSpides":
            index = 9;
            break;
            case "JackClubs":
            index = 10;
            break;
            case "QueenClubs":
            index = 11;
            break;
            case "QueenHearts":
            index = 12;
            break;
            case "AceDiamonds":
            index = 13;
            break;
            case "AceHearts":
            index = 14;
            break;
            case "AceClubs":
            index = 15;
            break;
            case "Sixteen":
            index = 16;
            break;
            default:
            index = 0;
            break;
        }

        return cardSprites[index];
    }

    private void PrepeareRound(string roundName)
    {
        Transform Round1Info = gamingTable.Find(roundName);

        for (int i = 0; i < Round1Info.childCount; i++ )
        {
            Transform child = Round1Info.GetChild(i);
            NewCard(new Vector3(), child, CardSpriteByName(child.name));
        }
    }

    private bool CardPicked()
    {
        if (!cardWaiting)
            return true;

        if (cardPicked)
        {
            cardPicked = false;
            cardWaiting = false;
            return true;
        }

        return cardPicked;

    }

    public bool CardWaiting()
    {
        return cardWaiting;
    }

    public override void OnSceneEvent(string eventName)
    {
        switch (eventName)
        {
            case "QueenTableTrigger":
            player.ForceVelocity(Vector2.zero);
            break;

            case "DeckCardTrigger":
                if (cardWaiting)
                    cardPicked = true;
            break;

            case "LevelComplete":
            G.SwitchScene(Scenes.MainMenu);
            break;
        }
    }
    public void SetCardsIntensity(float levelIndex)
    {
        levelIndex = Mathf.Clamp(levelIndex, 0, 2); // 0-2 for 3 labels
        RuntimeManager.StudioSystem.setParameterByName(globalParameterName, levelIndex);
    }
}
