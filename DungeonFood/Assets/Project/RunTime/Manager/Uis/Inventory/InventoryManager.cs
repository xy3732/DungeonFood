using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [field: SerializeField] public GameObject invetoryUiObject{ get; set; }

    [field: Header("Ui Manager")]
    [field: SerializeField] public InventoryUiManager inventoryUiManager { get; set; }
    private UiManager uiManager { get; set; }

    private void Start()
    {
        uiManager = UiManager.instance; 
    }

    // TabUis - Invetory ��ư���� ȣ��
    public void OnInvetory()
    {
        uiManager.offUi = OffInvetory;
        uiManager.previousUi = TabManager.instance.OnTab;

        // board �ؽ�Ʈ ����
        uiManager.SetBoardText("�κ��丮","esc - ��������");

        invetoryUiObject.SetActive(true);
        inventoryUiManager.OnInventoryUis();
    }

    public void OffInvetory()
    {
        inventoryUiManager.OffInventoryUis(invetoryUiObject);
    }
}
