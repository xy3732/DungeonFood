using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using GamePix.Ui;

public class CutEffectManager : MonoBehaviour
{
    [field: SerializeField] public GameObject cutEffectObject { get; set; }

    private RectTransform cutEffectTransform;
    private Sequence sequence;

    private void Awake()
    {
        cutEffectTransform = cutEffectObject.GetComponent<RectTransform>();
    }

    private void setClear()
    {
        cutEffectObject.SetActive(false);
    }

    // 가운데 에서 위,아레,왼쪽, 오른쪽으로 이동
    public void CenterTo(Vector3 toVector, Ease ease)
    {
        sequence.Kill();
        sequence = DOTween.Sequence().
            OnStart(() =>
            {
                cutEffectTransform.localPosition = UiVector3.Center;
                cutEffectObject.SetActive(true);
            })
            .Append
            (
                cutEffectTransform.DOLocalMove(toVector, 0.6f).SetEase(ease).OnComplete( () => setClear())
            );
    }

    // 위,아레,왼쪽, 오른쪽에서 가운데로 이동
    public void CenterFrom(Vector3 toVector, Ease ease)
    {
        sequence.Kill();
        sequence = DOTween.Sequence().
            OnStart(() =>
            {
                cutEffectTransform.localPosition = toVector;
                cutEffectObject.SetActive(true);
            })
            .Append
            (
                cutEffectTransform.DOLocalMove(UiVector3.Center, 0.6f).SetEase(ease).OnComplete(() => { })
            );
    }
}
