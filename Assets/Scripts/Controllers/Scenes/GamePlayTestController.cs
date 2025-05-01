using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTestController : SceneController
{
    [SerializeField] private CatDialogController catDialogController;

    private void Start()
    {
        StartCoroutine(ShowDialogTest());
    }

    private IEnumerator ShowDialogTest()
    {
        yield return new WaitForSeconds(3f);
        catDialogController.Show("Тестовое сообщение 1$$$И еще одно сообщение!");
    }
}
