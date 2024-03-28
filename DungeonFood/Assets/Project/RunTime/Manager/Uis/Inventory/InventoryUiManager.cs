using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GamePix.Ui;
using DG.Tweening;

public class InventoryUiManager : MonoBehaviour
{
    public Image testIamge;

    public void OnInventoryUis()
    {
        testIamge.gameObject.SetActive(true);

        testIamge.color = UiColor.AlphaZero;
        testIamge.DOColor(UiColor.AlphaOne, 0.5f)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true);
    }

    public void OffInventoryUis(GameObject uiObject)
    {
        testIamge.DOColor(UiColor.AlphaZero, 0.15f)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true)
            .OnComplete( () => 
            { 
                testIamge.gameObject.SetActive(false);
                uiObject.SetActive(false);
            });
    }
}
