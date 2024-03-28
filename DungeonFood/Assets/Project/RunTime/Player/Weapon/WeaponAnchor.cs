using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;

public class WeaponAnchor : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject weaponObject;

    private Player player;

    [field: Header("Position")]
    [field: SerializeField] private Vector3 anchor;
    private Vector3 splitVector;

    private void Awake()
    {
        playerObject = Player.instance.gameObject;
        weaponObject = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        player = Player.instance;
    }

    // LateUpdate �� �̵� ����
    // �̵� �Ϸ��� ������Ʈ
    private void LateUpdate()
    {
        splitVector = MousePosCheck();
        AnchorPosToPlayer(splitVector);
        MouseLookUp();
        FlipSprite(splitVector);
    }

    // ��Ŀ �۾�
    private void AnchorPosToPlayer(Vector3 _flip)
    {
        Vector3 FlipAnchor;

        if(_flip == FlipVector3.Left)
        {
            FlipAnchor = new Vector3(anchor.x * -1, anchor.y, anchor.z);
        }
        else
        {
            FlipAnchor = anchor;
        }

        transform.position = playerObject.transform.position + FlipAnchor;
    }

    // ���콺 ��ġ üũ
    private Vector3 MousePosCheck()
    {
        if (transform.rotation.eulerAngles.z > 90
           && transform.rotation.eulerAngles.z < 270)
        {
            return FlipVector3.Left;
        }
        else
        {
            return FlipVector3.Right;
        }
    }

    // ���콺 ���� �ٶ󺸱�
    private void MouseLookUp()
    {
        GameManager gameManager = GameManager.instance;

        var angle = Mathf.Atan2
            (
                gameManager.mousePos.y - transform.position.y,
                gameManager.mousePos.x - transform.position.x
            ) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // ��������Ʈ �ø�
    private void FlipSprite(Vector3 flipVector)
    {
        if(transform.rotation.eulerAngles.z > 90
            && transform.rotation.eulerAngles.z <270)
        {
            weaponObject.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            weaponObject.transform.localScale = new Vector3(1, 1, 1);
        }

        player.FlipSprite(flipVector);
    }
}
