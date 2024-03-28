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

    // ����� Ui ����
    private void Awake()
    {
        for (int i=0; i< onlyButtonImages.Length; i++)
        {
            // UiEffect �������̽� �� ������ ����Ʈ�� �߰�
            IUiEffectPos temp = onlyButtonImages[i]?.GetComponent<IUiEffectPos>();
            IButtons.Add(temp);
            buttonRectList.Add(buttonImages[i].GetComponent<RectTransform>());

            // ��ư ����Ʈ�� ���� ����Ʈ ������ ��ġ�� ����
            buttonRectList[i].anchoredPosition = temp.effectPos;
        }

        for (int i = 0; i < characterImages.Length; i++)
        {
            IUiEffectPos temp = characterImages[i]?.GetComponent<IUiEffectPos>();

            ICharacter.Add(temp);
        }
    }

    // ����� TabUi ��ä ����
    public void OnTabUis()
    {
        // init
        foreach (var item in buttonImages)
        {
            item.gameObject.SetActive(true);
            item.color = UiColor.AlphaZero;
        }

        // ��ư �̵� ����Ʈ
        for(int i=0; i<buttonRectList.Count; i++)
        {
            buttonRectList[i].DOAnchorPos(IButtons[i].normalPos, 0.3f).
                SetEase(Ease.OutCirc).
                SetUpdate(true);
        }

        // ��ü ������� faade ����Ʈ
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].DOKill();
            buttonImages[i].DOColor(UiColor.AlphaOne, i * 0.25f)
                .SetEase(Ease.OutExpo)
                .SetUpdate(true);
        }
    }

    // ĳ���� effect Pos �̵�
    public void CharacterMoveEffect()
    {
        for(int i=0;i< characterImages.Length; i++)
        {
            characterImages[i].rectTransform.DOAnchorPos(ICharacter[i].effectPos, 0.3f)
                .SetEase(Ease.OutQuint)
                .SetUpdate(true);
        }
    }

    // ĳ���� normal Pos �̵�
    public void CharacterMoveNormal()
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            characterImages[i].rectTransform.DOAnchorPos(ICharacter[i].normalPos, 0.3f)
                .SetEase(Ease.OutQuint)
                .SetUpdate(true);
        }
    }

    // ��ư�� Ȱ��ȭ
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

    // ��ư�� ��Ȱ��ȭ
    public void OnlyOffButtons()
    {
        foreach (var item in onlyButtonImages)
        {
            item.DOColor(UiColor.AlphaZero, 1 * 0.15f)
                .OnComplete( () => { item.gameObject.SetActive(false); })
                .SetUpdate(true);
        }
    }

    // ����� Ui �����
    public void OffTabUis()
    {
        // ��ư �̵� ����Ʈ
        for (int i = 0; i < buttonRectList.Count; i++)
        {
            buttonRectList[i].DOAnchorPos(IButtons[i].effectPos, 0.15f).
                SetEase(Ease.InExpo).
                SetUpdate(true);
        }

        // ��ư Fade ����Ʈ
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
