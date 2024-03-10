using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector3> move { get; set; }
    public Action idle { get; set; }

    private void FixedUpdate()
    {
        // WASD 등 이동 관련 키 입력 확인
        GetMovementKeyInput();
    }

    // WASD 등 이동 관련 키 입력 확인
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
