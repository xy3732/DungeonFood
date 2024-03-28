using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GamePix.Ui;

public class StateUi : MonoBehaviour
{
    [field: Header("Ui Object")]
    [field: SerializeField] private Image characterImage { get; set; }
    [field: SerializeField] private Image characterEffect { get; set; }

    [field: Header("character sprite")]
    [field: SerializeField] private Sprite characterNormal { get; set; }
    [field: SerializeField] private Sprite characterHit { get; set; }
    [field: SerializeField] private Sprite characterBlink { get; set; }

    static Vector3 nrmVector3 = new Vector3(0.65f, 0.65f, 0.65f);
    Vector3 nrmPos3;

    private void Start()
    {
        nrmPos3 = characterImage.rectTransform.anchoredPosition;
    }

    // 트윈 킬
    private void DotKill()
    {
        characterEffect.DOKill();
        characterImage.transform.DOKill();
    }

    public void OnHit()
    {
        DotKill();

        // 트윈 시작
        characterEffect.color = UiColor.AlphaOne;
        characterEffect.DOColor(UiColor.AlphaZero, 0.6f).SetEase(Ease.OutQuint);

        characterImage.sprite = characterHit;
        characterImage.transform.DOShakePosition(0.6f,3)
            .OnComplete(() => { OnNrm(); });

    }

    public void OnNrm()
    {
        DotKill();

        // 트윈 시작
        characterEffect.color = UiColor.AlphaZero;

        characterImage.rectTransform.anchoredPosition = nrmPos3; 
        characterImage.sprite = characterNormal;
        characterImage.transform.DOScale(nrmVector3, 3f)
            .OnComplete(() => { OnBlink(); });
    }

    private void OnBlink()
    {
        DotKill();

        // 트윈 시작
        characterImage.sprite = characterBlink;
        characterImage.transform.DOScale(nrmVector3, 0.3f)
            .OnComplete(() => { OnNrm(); });
    }
}
