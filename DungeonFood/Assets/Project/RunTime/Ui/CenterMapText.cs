using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CenterMapText : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    private Sequence sequence;

    [HideInInspector] public string text;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    // ������ ���� ����
    private void OnEnable()
    {
        float duration = 0.65f;

        tmpText.color = new Color(1,1,1,1);
        tmpText.text = "";

        sequence.Kill();
        gameObject.transform.DOKill();

        sequence = DOTween.Sequence().OnStart(() =>
        {
            gameObject.transform.DOScale(Vector3.one, (duration * text.Length) *0.5f)
            .OnComplete(() =>
            {
                tmpText.DOColor(new Color(1,1,1,0),1f).OnComplete( () => { gameObject.SetActive(false); });
            });
        });

        // 0.8�� �Ŀ� ���Ϳ� �ؽ�Ʈ ����
        gameObject.transform.DOScale(Vector3.one, 0.8f).OnComplete(() =>
        {
            StartCoroutine(UiManager.instance.Typing(tmpText, text, duration));
        });
    }

    public void Set(string _text)
    {
        text = _text;
        gameObject.SetActive(true);
    }
}
