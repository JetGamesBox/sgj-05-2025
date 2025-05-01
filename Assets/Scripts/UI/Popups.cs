using System.Collections;
using System.Collections.Generic;

using UnityEditor.PackageManager;

using UnityEngine;

public class Popups
{
    public static void Show(GameObject popup)
    {
        if (popup.activeSelf)
            return;

        popup.SetActive(true);
        popup.transform.SetAsLastSibling();
    }
}
