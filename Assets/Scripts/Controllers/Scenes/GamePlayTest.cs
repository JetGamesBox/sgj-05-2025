using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTestController : SceneController
{
    [SerializeField] private InteractiveDialogController DialogController;

    private void Start()
    {
        StartCoroutine(ShowDialogTest());
    }

    private IEnumerator ShowDialogTest()
    {
        yield return new WaitForSeconds(3f);
        DialogController?.Show(DialogPersones.Cat, "Тестовое сообщение 1$$$И еще одно сообщение!");
    }
}
