using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;

public class UiManager : Singleton<UiManager>
{
    [field: Header("UI")]
    [field: SerializeField] public CenterMapText MapTextUI;

    private static UiManager inst;
    public static CutEffectManager cutEffect;

    private void Awake()
    {
        cutEffect = GetComponent<CutEffectManager>();
        DontDestroySet();
    }


    // 씬 로드시 오브젝트 삭제 방지
    private void DontDestroySet()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Typing(TextMeshProUGUI tmpText, string message, float duration)
    {
        tmpText.text = message;
        TypingTextEffect(tmpText, duration);

        yield return null; 
    }

    private static void TypingTextEffect(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To( x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}
