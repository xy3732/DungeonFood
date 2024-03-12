using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class CmSector : MonoBehaviour
{
    private GameObject childObject;

    [field: Header("Map Data")]
    [field: SerializeField] private string mapName;

    [field: Header("Map Effect")]
    [field: SerializeField] private GameObject mapEffectObject;

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

        // �ش� �ʿ� ���� ���̸� ����
        UiManager.instance.MapTextUI.Set(mapName);

        childObject.SetActive(true);
        mapEffectObject?.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        childObject.SetActive(false);
        mapEffectObject?.SetActive(false);
    }
}
