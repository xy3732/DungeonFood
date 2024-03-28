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

    // TabUis - Invetory 버튼으로 호출
    public void OnInvetory()
    {
        uiManager.offUi = OffInvetory;
        uiManager.previousUi = TabManager.instance.OnTab;

        // board 텍스트 설정
        uiManager.SetBoardText("인벤토리","esc - 이전으로");

        invetoryUiObject.SetActive(true);
        inventoryUiManager.OnInventoryUis();
    }

    public void OffInvetory()
    {
        inventoryUiManager.OffInventoryUis(invetoryUiObject);
    }
}
