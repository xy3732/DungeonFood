using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

#if UNITY_EDITOR
[ExecuteInEditMode()]
#endif

[RequireComponent(typeof(VerticalLayoutGroup))]
[RequireComponent(typeof(ContentSizeFitter))]
[RequireComponent(typeof(LayoutElement))]

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headText;
    public TextMeshProUGUI mainText;

    [Space(20)]
    public LayoutElement layoutElement;
    public RectTransform rect;

    [Space(20)]
    public int characterWrapLimit;

    public void Awake()
    {
        rect = GetComponent<RectTransform>();  
    }

    // 툴팁 텍스트
    public void SetText(string context, string header = "")
    {
        // 해더의 텍스트 값이 존재하지 않으면 오브젝트 비활성화
        if (string.IsNullOrEmpty(header))
        {
            headText.gameObject.SetActive(false);
        }
        // 그렇지 않으면 해더 오브젝트 활성화
        else
        {
            headText.gameObject.SetActive(true);
            headText.text = header;
        }

        // context의 텍스트 설정
        mainText.text = context;

        // 해더 및 메인의 텍스트 글자 길이
        int headLength = headText.text.Length;
        int contextLength = mainText.text.Length;

        // 해당 글자가 리미트 값보다 높다면 layoutElement 활성화
        layoutElement.enabled =
            (headLength > characterWrapLimit || contextLength > characterWrapLimit)
            ? true : false;
    }

    public void Update()
    {
        // 유니티 에디터에서만 활성화
        // 빌드시 실행안됨
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            int headerLength = headText.text.Length;
            int contentLength = mainText.text.Length;

            // 만약 리미트 건 숫자보다 높으면 layoutElement 활성화
            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
#endif
        // 마우스 현재 값 가져오기
        Vector2 position = Input.mousePosition;

        // 툴팁의 앵커값 조정
        float AnchorX = (position.x > (Screen.width * 0.5f)) ? 1f : -0.15f;
        // 툴팁 화면 밬으로 못나가게 설정
        float pivotY = position.y / Screen.height;

        // 툴팁 위치 변경
        rect.pivot = new Vector2(AnchorX, pivotY);
        transform.position = position;

    }
}
