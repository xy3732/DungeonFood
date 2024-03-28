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
            Debug.LogWarning("해당 오브젝트에 이미 다른 버튼 스크립트가 있습니다.");
        }
    }
    private void Clicked()
    {
        // 현재 버튼이 눌렸는지 확인
        isClicked = !isClicked;

        // 유니티 클릭 이벤트가 있을시에만 실행
        if (onClick != null)
        {
            onClick.Invoke();
        }

        // 임시 컬러값 저장
        Color32 tempColor;

        // 토글 버튼 일시에만 실행
        if (Togle)
        {
            tempColor = isClicked ? pressedColor : highlightColor;
        }
        // 일반적인 버튼
        else tempColor = pressedColor;

        // 버튼 배경색 지정
        background.color = tempColor;
    }

    // - IPointerClickHandler -> 해당 버튼 클릭시 실행
    public void OnPointerClick(PointerEventData eventData)
    {
        // 토글 버튼일시 실행
        if (Togle) Clicked();
        else
        {
            if (onClick != null)
            {
                onClick.Invoke();
            }
        }
    }

    // - IPointerUpHandler -> 해당 버튼 떼는 순간 실행
    public void OnPointerUp(PointerEventData eventData)
    {
        // 토글 버튼이 아닐시 실행
        // isIn이 없으면 해당 버튼을 누르고 버튼 밬으로 나가도 실행이 된다.
        if (!Togle && isIn) background.color = highlightColor;
    }

    // - IPointerDownHandler -> 해당 버튼 클릭중일때 실행
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0);
        // 토글 버튼이 아닐시 실행
        if (!Togle) background.color = pressedColor;
    }

    // - IPointerEnterHandler -> 해당 버튼 안으로 들어오면 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            background.color = highlightColor;
        }
        // 마우스 누르고 밬으로 나가면 버튼 실행이 안되게 설정
        isIn = true;

        if (onImage)
        {
            background.sprite = onImage;
        }

        onHover.Invoke();
        transform.DOScale(TweenSize, 0.1f);
    }

    // -  IPointerExitHandler -> 해당 버튼 밬으로 나가면 실행
    public void OnPointerExit(PointerEventData eventData)
    {
        // 트글 버튼이 아니거나 클릭한 버튼이 아니면 노말값 색상으로 변경
        if (!isClicked || !Togle) background.color = normalColor;
        isIn = false;

        onExit.Invoke();

        background.sprite = normalImage;
        transform.DOScale(Vector3.one, 0.1f);
    }
}
