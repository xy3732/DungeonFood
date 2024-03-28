using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
    public Action<Vector3> move { get; set; }
    public Action idle { get; set; }

    public Action escape { get; set; }
    public Action tab { get; set; }

    
    private void Update()
    {
        // Tab키 입력 확인
        GetTabBtns();
    }

    // 물리적 처리만 여기서 실행
    private void FixedUpdate()
    {
        // WASD 등 이동 관련 키 입력 확인
        GetMovementKeyInput();
    }

    // tab버튼 누를시 실행
    // UiManager로 토스
    private void GetTabBtns()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tab?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escape?.Invoke();
        }
    }

    // WASD 등 이동 관련 키 입력 확인
    // Player로 토스
    private void GetMovementKeyInput()
    {
        // WASD
        if (Input.GetKey(KeyCode.A))
        {
            move?.Invoke(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move?.Invoke(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            move?.Invoke(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            move?.Invoke(Vector3.down);
        }
        // 아무것도 입력을 안할시 IDLE 
        else
        {
            idle?.Invoke();
        }
    }
}
