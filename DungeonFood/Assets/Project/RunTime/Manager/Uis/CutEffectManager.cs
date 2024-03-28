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

    // ��� ���� ��,�Ʒ�,����, ���������� �̵�
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

    // ��,�Ʒ�,����, �����ʿ��� ����� �̵�
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
