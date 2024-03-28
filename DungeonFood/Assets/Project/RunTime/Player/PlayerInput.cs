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
        // TabŰ �Է� Ȯ��
        GetTabBtns();
    }

    // ������ ó���� ���⼭ ����
    private void FixedUpdate()
    {
        // WASD �� �̵� ���� Ű �Է� Ȯ��
        GetMovementKeyInput();
    }

    // tab��ư ������ ����
    // UiManager�� �佺
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

    // WASD �� �̵� ���� Ű �Է� Ȯ��
    // Player�� �佺
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
        // �ƹ��͵� �Է��� ���ҽ� IDLE 
        else
        {
            idle?.Invoke();
        }
    }
}
