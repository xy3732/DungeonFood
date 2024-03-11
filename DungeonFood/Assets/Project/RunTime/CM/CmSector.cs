using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class CmSector : MonoBehaviour
{
    private GameObject childObject;  

    private void Awake()
    {
        // �ڽ� ������Ʈ�� ù��° ������Ʈ�� �����´�
        childObject = transform.GetChild(0).gameObject;
        childObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        childObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        childObject.SetActive(false);
    }
}
