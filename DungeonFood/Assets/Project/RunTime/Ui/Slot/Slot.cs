using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using GamePix.Ui;
using UnityEngine.Events;
using DG.Tweening;



#if UNITY_EDITOR
using UnityEditor;
public class ESlot : Editor
{
    [MenuItem("GameObject/GamePix/Ui/Slot", false, 1)]

    public static void CreateButtons(MenuCommand menuCommand)
    {

    }
}
#endif

[AddComponentMenu("GamePix/Ui/Slot")]
public class Slot : MonoBehaviour,
    IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Image")]
    public Sprite normalImage;
    public Sprite onImage;

    [Header("settings")]
    public float onEnableDelay = 0f;
    public Vector3 TweenSize = new Vector3(1, 1, 1);
    public bool Togle = false;

    [Header("Colors")]
    public Color32 normalColor = UiColor.white;
    public Color32 highlightColor = new Color32(200, 200, 200, 255);
    public Color32 pressedColor = new Color32(160, 160, 160, 255);
    public Color32 selectedColor = UiColor.white;
    public Color32 disableColor = UiColor.white;

    [Header("UnityEvent")]
    public UnityEvent onClick;
    public UnityEvent onHover;
    public UnityEvent onExit;

    Image background;

    bool isClicked = false;
    bool isIn = false;

    private void Awake()
    {
        ErrorChecker();

        background = GetComponent<Image>();

        background.sprite = normalImage;
        background.color = normalColor;
    }

    private void OnEnable()
    {
        background.sprite = normalImage;
    }

    private void ErrorChecker()
    {
        if (GetComponent<Button>() != null)
        {
            Debug.LogWarning("�ش� ������Ʈ�� �̹� �ٸ� ��ư ��ũ��Ʈ�� �ֽ��ϴ�.");
        }
    }
    private void Clicked()
    {
        // ���� ��ư�� ���ȴ��� Ȯ��
        isClicked = !isClicked;

        // ����Ƽ Ŭ�� �̺�Ʈ�� �����ÿ��� ����
        if (onClick != null)
        {
            onClick.Invoke();
        }

        // �ӽ� �÷��� ����
        Color32 tempColor;

        // ��� ��ư �Ͻÿ��� ����
        if (Togle)
        {
            tempColor = isClicked ? pressedColor : highlightColor;
        }
        // �Ϲ����� ��ư
        else tempColor = pressedColor;

        // ��ư ���� ����
        background.color = tempColor;
    }

    // - IPointerClickHandler -> �ش� ��ư Ŭ���� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // ��� ��ư�Ͻ� ����
        if (Togle) Clicked();
        else
        {
            if (onClick != null)
            {
                onClick.Invoke();
            }
        }
    }

    // - IPointerUpHandler -> �ش� ��ư ���� ���� ����
    public void OnPointerUp(PointerEventData eventData)
    {
        // ��� ��ư�� �ƴҽ� ����
        // isIn�� ������ �ش� ��ư�� ������ ��ư �U���� ������ ������ �ȴ�.
        if (!Togle && isIn) background.color = highlightColor;
    }

    // - IPointerDownHandler -> �ش� ��ư Ŭ�����϶� ����
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0);
        // ��� ��ư�� �ƴҽ� ����
        if (!Togle) background.color = pressedColor;
    }

    // - IPointerEnterHandler -> �ش� ��ư ������ ������ ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            background.color = highlightColor;
        }
        // ���콺 ������ �U���� ������ ��ư ������ �ȵǰ� ����
        isIn = true;

        if (onImage)
        {
            background.sprite = onImage;
        }

        onHover.Invoke();
        transform.DOScale(TweenSize, 0.1f);
    }

    // -  IPointerExitHandler -> �ش� ��ư �U���� ������ ����
    public void OnPointerExit(PointerEventData eventData)
    {
        // Ʈ�� ��ư�� �ƴϰų� Ŭ���� ��ư�� �ƴϸ� �븻�� �������� ����
        if (!isClicked || !Togle) background.color = normalColor;
        isIn = false;

        onExit.Invoke();

        background.sprite = normalImage;
        transform.DOScale(Vector3.one, 0.1f);
    }
}
