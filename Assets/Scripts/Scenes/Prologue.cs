using System;
using System.Collections;
using UnityEngine;

public class Prologue : SceneController
{
    [SerializeField] Transform container;

    private bool clicked = false;

    void Start()
    {
        StartCoroutine(PrologueCutScene());
    }

    private IEnumerator PrologueCutScene()
    {
        G.input.Blocked = true;

        yield return Show(0);
        yield return Show(1);
        yield return Show(2);
        yield return Show(3);
        yield return Show(4, 5f);
        yield return Show(5);
        yield return Show(6);

        container.GetChild(7).gameObject.SetActive(true);

        yield return new WaitUntil(CheckClick);

        yield return Show(8);

        G.SwitchScene(Scenes.Level1);
    }

    private WaitForSeconds Show(int index, float delay = 3f)
    {
        container.GetChild(index).gameObject.SetActive(true);

        return new WaitForSeconds(delay);
    }

    private bool CheckClick()
    {
        return clicked;
    }

    public override void OnSceneEvent(string eventName)
    {
        clicked = true;
    }
}
