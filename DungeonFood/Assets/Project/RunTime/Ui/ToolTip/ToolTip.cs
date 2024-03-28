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

    // ���� �ؽ�Ʈ
    public void SetText(string context, string header = "")
    {
        // �ش��� �ؽ�Ʈ ���� �������� ������ ������Ʈ ��Ȱ��ȭ
        if (string.IsNullOrEmpty(header))
        {
            headText.gameObject.SetActive(false);
        }
        // �׷��� ������ �ش� ������Ʈ Ȱ��ȭ
        else
        {
            headText.gameObject.SetActive(true);
            headText.text = header;
        }

        // context�� �ؽ�Ʈ ����
        mainText.text = context;

        // �ش� �� ������ �ؽ�Ʈ ���� ����
        int headLength = headText.text.Length;
        int contextLength = mainText.text.Length;

        // �ش� ���ڰ� ����Ʈ ������ ���ٸ� layoutElement Ȱ��ȭ
        layoutElement.enabled =
            (headLength > characterWrapLimit || contextLength > characterWrapLimit)
            ? true : false;
    }

    public void Update()
    {
        // ����Ƽ �����Ϳ����� Ȱ��ȭ
        // ����� ����ȵ�
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            int headerLength = headText.text.Length;
            int contentLength = mainText.text.Length;

            // ���� ����Ʈ �� ���ں��� ������ layoutElement Ȱ��ȭ
            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
#endif
        // ���콺 ���� �� ��������
        Vector2 position = Input.mousePosition;

        // ������ ��Ŀ�� ����
        float AnchorX = (position.x > (Screen.width * 0.5f)) ? 1f : -0.15f;
        // ���� ȭ�� �U���� �������� ����
        float pivotY = position.y / Screen.height;

        // ���� ��ġ ����
        rect.pivot = new Vector2(AnchorX, pivotY);
        transform.position = position;

    }
}
