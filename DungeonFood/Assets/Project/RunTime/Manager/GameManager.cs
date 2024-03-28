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
        // 마우스 포지션 위치
        var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;
        mousePos = screenPos;
    }

    // 타입스케일 0 으로 설정
    public void TimeScaleSet(float index)
    {
        Time.timeScale = index;
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
