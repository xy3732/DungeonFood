using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class CmSector : MonoBehaviour
{
    private GameObject childObject;  

    private void Awake()
    {
        // 자식 오브젝트중 첫번째 오브젝트를 가져온다
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
