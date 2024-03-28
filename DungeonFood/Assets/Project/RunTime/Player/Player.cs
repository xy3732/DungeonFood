using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;

public class Player : Singleton<Player>
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
        // playerInput으로 부터 가져오기
        playerInput.move = Move;
        playerInput.idle = Idle;
    }

    // 이니셜라이저 - 초기화
    private void init()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    // 플레이어 이동
    private void Move(Vector3 dir)
    {
        Vector3 velocity = MoveTo(dir);

        // 6 - 플레이어 이동 속도 (scriptableObject data로 만들예정)
        rigid.MovePosition(rigid.position + (Vector2)velocity * 6 * Time.smoothDeltaTime );
    }

    // 스프라이트 플립
    public void FlipSprite(Vector3 flip)
    {
        transform.localScale = flip;
    }

    // 캐릭터 이동 로직
    private Vector3 MoveTo(Vector3 velocity)
    {
        animator.SetBool("isMove",true);

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

    // 플레이어 Idle
    private void Idle()
    {
        rigid.velocity = Vector2.zero;
        animator.SetBool("isMove",false);
    }

}
