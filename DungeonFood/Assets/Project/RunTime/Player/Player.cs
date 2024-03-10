using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rigid;
    private Animator animator;

    private Vector3 direction = FlipVector3.Default;

    private void Awake()
    {
        init();
    }

    private void Start()
    {
        // playerInput���� ���� ��������
        playerInput.move = Move;
        playerInput.idle = Idle;
    }

    // �̴ϼȶ����� - �ʱ�ȭ
    private void init()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    // �÷��̾� �̵�
    private void Move(Vector3 dir)
    {
        Vector3 velocity = MoveTo(dir);
        Flip();

        // 6 - �÷��̾� �̵� �ӵ� (scriptableObject data�� ���鿹��)
        rigid.MovePosition(rigid.position + (Vector2)velocity * 6 * Time.smoothDeltaTime );
    }

    private void Flip()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.localScale = FlipVector3.Left;
        }
        if(Input.GetKey(KeyCode.D)) 
        {
            transform.localScale = FlipVector3.Right;
        }
    }

    // ĳ���� �̵� ����
    private Vector3 MoveTo(Vector3 velocity)
    {
        animator.SetBool("isMove",true);

        Debug.Log(velocity);

        if (Input.GetKey(KeyCode.S))
        {
            velocity = (velocity + Vector3.down).normalized;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            velocity = (velocity + Vector3.up).normalized;
        }

        return velocity;
    }

    // �÷��̾� Idle
    private void Idle()
    {
        rigid.velocity = Vector2.zero;
        animator.SetBool("isMove",false);
    }

}
