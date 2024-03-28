using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameManager inst;

    [field: HideInInspector] public Vector3 mousePos { get; set; }

    [field: Header("Vcam")]
    [field: SerializeField] public GameObject vCamMouseTarget { get; set; }

    public void test()
    {

    }
    private void Awake()
    {
        DontDestroySet();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            UiManager.instance.stateUi.OnHit();
        }
    }

    private void FixedUpdate()
    {
        // ���콺 ������ ��ġ
        var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;
        mousePos = screenPos;
    }

    // Ÿ�Խ����� 0 ���� ����
    public void TimeScaleSet(float index)
    {
        Time.timeScale = index;
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
