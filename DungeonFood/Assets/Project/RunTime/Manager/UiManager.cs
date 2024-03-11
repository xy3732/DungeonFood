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

    // �� �ε�� ������Ʈ ���� ����
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
