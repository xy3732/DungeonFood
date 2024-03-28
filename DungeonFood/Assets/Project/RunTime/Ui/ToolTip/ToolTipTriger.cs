using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;
using TMPro;

public class ToolTipTriger : MonoBehaviour,
IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text")]
    public string header;
    [Multiline()]
    public string context;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScaleZ(1, 0.6f).OnComplete(
            () =>
            ToolTipSystem.Show(context,header)
            ).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        ToolTipSystem.Hide();
    }
}
