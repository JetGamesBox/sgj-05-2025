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

        G.ShowSceneDialog(DialogPersones.Cat, "Остался последний архиватор!$$$Кажется его никто не охраняет...", 2f);

        yield return new WaitForSeconds(5f);

        G.CameraFocus(queen.transform);
        G.ShowSceneDialog(DialogPersones.Queen, "Проваливай копаться в песочке, хвостатый!", 2.5f);

        yield return new WaitForSeconds(2.5f);

        G.ShowSceneDialog(DialogPersones.Cat, "Ой-ей-ей...", 2f);

        yield return new WaitForSeconds(2f);

        G.ShowSceneDialog(DialogPersones.Queen, "А ты, девочка, подойди сюда!", 2f);

        yield return new WaitForSeconds(3f);

        G.CameraFocus(player.transform);

        G.input.Blocked = false;
    }

    private IEnumerator CutSceneLevelTestBegin()
    {
        G.input.Blocked = true;

        G.CameraFocus(queen.transform);
        G.ShowSceneDialog(DialogPersones.Queen, "Что ты делаешь в даркнете?$$$Это не место для таких как ты.", 2f);

        yield return new WaitForSeconds(5f);

        G.CameraFocus(player.transform);
        G.ShowSceneDialog(DialogPersones.Alice, "Я попала сюда случайно! И хочу выбраться!", 3f);

        yield return new WaitForSeconds(3f);

        G.CameraFocus(queen.transform);
        G.ShowSceneDialog(DialogPersones.Queen, "Хмм... Я могу тебе помочь...$$$Если ты выйграешь у меня в \"двадцать-одно\"!$$$Правила уж должна знать, раз ты здесь.", 3f);

        yield return new WaitForSeconds(10f);

        G.ShowSceneDialog(DialogPersones.Cat, "Она что то замышляет, но другого варианта нет...", 4f);

        yield return new WaitForSeconds(4f);

        gamingTable.gameObject.SetActive(true);

        G.CameraFocus(gamingTable.transform, 8.15f);
        G.ShowSceneDialog(DialogPersones.Queen, "Тогда начнем!$$$Тяни карту.", 2f);
    }


    public override void OnSceneEvent(string eventName)
    {
        switch (eventName)
        {
            case "LevelTestBegin": StartCoroutine(CutSceneLevelTestBegin()); break;
        }
    }
}
