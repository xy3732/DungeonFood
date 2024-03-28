using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquimentManager : Singleton<EquimentManager>
{
    [field: SerializeField] public GameObject equimentUiObject { get; set; }

    [field: Header("Ui Manager")]
    [field: SerializeField] public EquimentUiManager equimentUiManager { get; set;}
    private UiManager uiManager { get; set; }

    [field: HideInInspector] public List<Slot> inventorySlot { get; set; }

    private void Start()
    {
        uiManager = UiManager.instance;
    }

    // TabUi - Equiment 버튼으로 호출
    public void OnEquiment()
    {
        uiManager.offUi = OffEquiment;
        uiManager.previousUi = TabManager.instance.OnEquimentToTab;

        // board 텍스트 설정
        uiManager.SetBoardText("장비창", "esc - 이전으로");

        equimentUiObject.SetActive(true);
        equimentUiManager.OOnEquimentUis();
        
    }

    public void OffEquiment()
    {
        equimentUiManager.OffEquimentUis(equimentUiObject);
    }
}
