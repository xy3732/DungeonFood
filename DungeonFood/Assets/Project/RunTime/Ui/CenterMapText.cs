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

    // 켜질때 마다 실행
    private void OnEnable()
    {
        // 섹터에 처음 들어간게 아닐시 리턴
        if(!UiManager.instance.firstIn)
        {
            return;
        }

        // 아니라면 섹터에 들어간걸로 판별후 이펙트 실행
        UiManager.instance.firstIn = false;

        // 타이핑 이펙트 속도
        float duration = 0.3f;

        tmpText.color = new Color(1,1,1,1);
        tmpText.text = "";

        sequence.Kill();
        gameObject.transform.DOKill();
        StopCoroutine(UiManager.instance.Typing(tmpText, text, duration));

        sequence = DOTween.Sequence().OnStart(() =>
        {
            gameObject.transform.DOScale(Vector3.one, (duration * text.Length) * 0.5f)
            .OnComplete(() =>
            {
                tmpText.DOColor(new Color(1, 1, 1, 0), 1f).OnComplete(() => { gameObject.SetActive(false); });
            });
        });

        // 0.8초 후에 센터에 텍스트 띄우기
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
