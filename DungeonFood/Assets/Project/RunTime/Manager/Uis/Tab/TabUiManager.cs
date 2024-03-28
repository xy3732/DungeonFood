using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using GamePix.Ui;

public class TabUiManager : MonoBehaviour
{
    [field: Header("Buttons")]
    [field: SerializeField] public Image[] buttonImages { get; set; }
    [field: SerializeField] public Image[] onlyButtonImages { get; set; }
    [field: SerializeField] public Image[] characterImages { get; set; }

    private List<RectTransform> buttonRectList = new List<RectTransform>();
    [field: SerializeField] List<IUiEffectPos> IButtons = new List<IUiEffectPos>();
    [field: SerializeField]List<IUiEffectPos> ICharacter = new List<IUiEffectPos>();

    // 실행시 Ui 띄우기
    private void Awake()
    {
        for (int i=0; i< onlyButtonImages.Length; i++)
        {
            // UiEffect 인터페이스 가 있으면 리스트에 추가
            IUiEffectPos temp = onlyButtonImages[i]?.GetComponent<IUiEffectPos>();
            IButtons.Add(temp);
            buttonRectList.Add(buttonImages[i].GetComponent<RectTransform>());

            // 버튼 이펙트를 위해 이펙트 스케일 위치로 설정
            buttonRectList[i].anchoredPosition = temp.effectPos;
        }

        for (int i = 0; i < characterImages.Length; i++)
        {
            IUiEffectPos temp = characterImages[i]?.GetComponent<IUiEffectPos>();

            ICharacter.Add(temp);
        }
    }

    // 실행시 TabUi 전채 띄우기
    public void OnTabUis()
    {
        // init
        foreach (var item in buttonImages)
        {
            item.gameObject.SetActive(true);
            item.color = UiColor.AlphaZero;
        }

        // 버튼 이동 이펙트
        for(int i=0; i<buttonRectList.Count; i++)
        {
            buttonRectList[i].DOAnchorPos(IButtons[i].normalPos, 0.3f).
                SetEase(Ease.OutCirc).
                SetUpdate(true);
        }

        // 전체 순서대로 faade 이펙트
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].DOKill();
            buttonImages[i].DOColor(UiColor.AlphaOne, i * 0.25f)
                .SetEase(Ease.OutExpo)
                .SetUpdate(true);
        }
    }

    // 캐릭터 effect Pos 이동
    public void CharacterMoveEffect()
    {
        for(int i=0;i< characterImages.Length; i++)
        {
            characterImages[i].rectTransform.DOAnchorPos(ICharacter[i].effectPos, 0.3f)
                .SetEase(Ease.OutQuint)
                .SetUpdate(true);
        }
    }

    // 캐릭터 normal Pos 이동
    public void CharacterMoveNormal()
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            characterImages[i].rectTransform.DOAnchorPos(ICharacter[i].normalPos, 0.3f)
                .SetEase(Ease.OutQuint)
                .SetUpdate(true);
        }
    }

    // 버튼만 활성화
    public void OnlyOnButtons()
    {
       for(int i=0; i<onlyButtonImages.Length; i++)
        {
            onlyButtonImages[i].gameObject.SetActive(true);

            onlyButtonImages[i].DOKill();
            onlyButtonImages[i].color = UiColor.AlphaZero;

            onlyButtonImages[i].DOColor(UiColor.AlphaOne, i * 0.25f)
                .SetEase(Ease.OutExpo)
                .SetUpdate(true);
        }
    }

    // 버튼만 비활성화
    public void OnlyOffButtons()
    {
        foreach (var item in onlyButtonImages)
        {
            item.DOColor(UiColor.AlphaZero, 1 * 0.15f)
                .OnComplete( () => { item.gameObject.SetActive(false); })
                .SetUpdate(true);
        }
    }

    // 실행시 Ui 지우기
    public void OffTabUis()
    {
        // 버튼 이동 이펙트
        for (int i = 0; i < buttonRectList.Count; i++)
        {
            buttonRectList[i].DOAnchorPos(IButtons[i].effectPos, 0.15f).
                SetEase(Ease.InExpo).
                SetUpdate(true);
        }

        // 버튼 Fade 이펙트
        foreach (var item in buttonImages)
        {
            item.DOColor(UiColor.AlphaZero, 1 * 0.15f)
                .SetUpdate(true);
        }

        gameObject.transform.DOScale(Vector3.one, 1 * 0.15f)
            .SetUpdate(true)
            .OnComplete(() => { gameObject.SetActive(false); });
    }
}
