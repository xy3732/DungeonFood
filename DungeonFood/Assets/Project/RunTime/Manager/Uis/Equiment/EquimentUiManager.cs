using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GamePix.Ui;
using DG.Tweening;

public class EquimentUiManager : MonoBehaviour
{
    [field: Header("Uis")]
    public Image[] uis;

    [field: Header("Grid Layout")]
    public GameObject containerObject;
    public GameObject gridObject;

    [field: Header("Prefab")]
    public Slot slotPrefab;

    private EquimentManager equimentManager;

    // init
    private void Start()
    {
        equimentManager = EquimentManager.instance;
        Debug.Log(equimentManager);

        for (int i = 0; i < 25; i++)
        {
            Slot slot = Instantiate(slotPrefab);
            slot.transform.SetParent(gridObject.transform);
            slot.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OOnEquimentUis()
    {
        containerObject.SetActive(true);
        foreach (var item in uis)
        {
            item.gameObject.SetActive(true);

            item.color = UiColor.AlphaZero;
            item.DOColor(UiColor.AlphaOne, 0.5f)
                .SetEase(Ease.OutExpo)
                .SetUpdate(true);
        }
    }

    public void OffEquimentUis(GameObject uiObject)
    {
        containerObject.SetActive(false);
        foreach (var item in uis)
        {
            item.DOColor(UiColor.AlphaZero, 0.15f)
           .SetEase(Ease.OutExpo)
           .SetUpdate(true)
           .OnComplete(() =>
           {
               item.gameObject.SetActive(false);
               uiObject.SetActive(false);
           });
        }
    }
}
