using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HoverButtonEffect : MonoBehaviour
{
    [field: Header("text")]
    public TextMeshProUGUI textUi;
    public string text;

    [field: Header("Object")]
    public GameObject[] uiObject;

    public void OnHover()
    {
        // 하위 오브젝트 활성화
        foreach (GameObject go in uiObject)
        {
            go.SetActive(true);
        }

        StartCoroutine(UiManager.instance.Typing(textUi, text, 0.15f));
    }

    public void OffHover()
    {
        // 하위 오브젝트 비활성화
        foreach (GameObject go in uiObject)
        {
            go.SetActive(false);
        }

        textUi.text = "";
        StopCoroutine(UiManager.instance.Typing(textUi, text, 0.15f));
    }
}
