using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
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
}
