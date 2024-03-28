using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : Singleton<TabManager>
{
    [field: SerializeField] public GameObject tabUiObject { get; set; }

    [field: Header("Ui Manager")]
    [field: SerializeField] public TabUiManager tabUiManager { get; set; }
    private UiManager uiManager { get; set; }

    private void Start()
    {
        uiManager = UiManager.instance;
    }

    public void OnTab()
    {
        Set();

        tabUiObject.SetActive(true);
        tabUiManager.OnTabUis();
    }

    public void OnEquimentToTab()
    {
        Set();

        tabUiObject.SetActive(true);
        tabUiManager.OnlyOnButtons();
        tabUiManager.CharacterMoveNormal();
    }

    private void Set()
    {
        // action �� �ش� Ui Off ����
        uiManager.offUi = OffTab;
        uiManager.previousUi = null;

        // board �ؽ�Ʈ ����
        uiManager.SetBoardText("���� �޴�", "esc - ��������");
    }

    public void OffTab()
    {
        tabUiObject.SetActive(false);
        tabUiManager.OffTabUis();
    }
}
