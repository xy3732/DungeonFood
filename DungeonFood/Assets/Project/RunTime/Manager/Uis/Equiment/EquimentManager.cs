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

    // TabUi - Equiment ��ư���� ȣ��
    public void OnEquiment()
    {
        uiManager.offUi = OffEquiment;
        uiManager.previousUi = TabManager.instance.OnEquimentToTab;

        // board �ؽ�Ʈ ����
        uiManager.SetBoardText("���â", "esc - ��������");

        equimentUiObject.SetActive(true);
        equimentUiManager.OOnEquimentUis();
        
    }

    public void OffEquiment()
    {
        equimentUiManager.OffEquimentUis(equimentUiObject);
    }
}
