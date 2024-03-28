using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;
using GamePix.Ui;
using System;

public class UiManager : Singleton<UiManager>
{
    private static UiManager inst;
    [field: Header("UI")]
    [field: SerializeField] public CenterMapText MapTextUI { get; set; }
    [field: SerializeField] public GameObject mainUis { get; set; }

    [field: SerializeField] public StateUi stateUi { get; set; }

    [field: Header("Board Obejct")]
    [field: SerializeField] public RectTransform topBoardRect { get; set; }
    [field: SerializeField] public RectTransform bottomBoardRect { get; set; }

    [field: Header("Board Text")]
    [field: SerializeField] private TextMeshProUGUI topText { get; set; }
    [field: SerializeField] private TextMeshProUGUI escText { get; set; }

    // Ui Manager ����Ʈ
    public static CutEffectManager cutEffect;
    public static TabManager tabManager;

    // Bool
    [field: HideInInspector] public bool onTab { get; set; }
    [field: HideInInspector] public bool firstIn { get; set; }

    //Action
    [field: HideInInspector] public Action offUi { get; set; }
    [field: HideInInspector] public Action previousUi { get; set; }

    // ��Ʈ�� ������
    private Sequence sequence;


    private void Awake()
    {
        onTab = false;
        firstIn = true;

        tabManager = GetComponent<TabManager>();
        cutEffect = GetComponent<CutEffectManager>();

        DontDestroySet();
    }

    private void Start()
    {
        PlayerInput.instance.tab = Tab;
        PlayerInput.instance.escape = Esacpe;
    }

    private void Esacpe()
    {

        // Tab Ui Ȱ��ȭ �������� �ش� Ui ������ �������� ���ư���
        if(onTab)
        {
            ToGameUi();
        }
        // �ƴϸ� �Ͻ����� Ui ����
        else
        {

        }
    }

    // Tab ����Ʈ
    private void Tab()
    {
        // ���� �ڷ�ƾ ����
        StopCoroutine(PostProcessManager.instance.BlurLerp(1, 1, 0f));

        onTab = !onTab;

        // tab ui ����Ʈ �� ������Ʈ - FALSE
        if (onTab) 
        {
            GameManager.instance.TimeScaleSet(0);
            tabManager.OnTab();
            SetUiObject(false);
            OnBoard();
            // Blur
            StartCoroutine(PostProcessManager.instance.BlurLerp(60, 100, 0.5f));
            SetMainUiActive(false);
        }
        // tab ui ����Ʈ �� ������Ʈ - TRUE
        else
        {
            GameManager.instance.TimeScaleSet(1);
            tabManager.OffTab();
            offUi?.Invoke();
            SetUiObject(true);
            OffBoard();
            // Blur
            StartCoroutine(PostProcessManager.instance.BlurLerp(100, 1, 0.65f));
            SetMainUiActive(true);
        }
    }

    // ���� ui ��Ʈ�� (active)
    private void SetMainUiActive(bool set)
    {
        mainUis.SetActive(set);
    }

    // Ui ������Ʈ Ȱ��ȭ ���� �κ�
    private void SetUiObject(bool _active)
    {
        MapTextUI.gameObject.SetActive(_active);
    }

    // �� �ε�� ������Ʈ ���� ����
    private void DontDestroySet()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToGameUi()
    {
        if (previousUi == null)
        {
            GameManager.instance.TimeScaleSet(1);

            onTab = false;
            OffBoard();
            SetUiObject(true);

            //Blur
            StartCoroutine(PostProcessManager.instance.BlurLerp(100, 1, 0.5f));
        }

        offUi?.Invoke();
        previousUi?.Invoke();
    }

    // Off -> On Ʈ���� (Board)
    public void OnBoard()
    {
        // ��Ʈ�� ������ ���۽� ������ ų (����)
        sequence.Kill();
        // ��Ʈ�� ������ �ʱ⼳��
        sequence = DOTween.Sequence().OnStart(() =>
        {
            topBoardRect.DOAnchorPosY(100, 0f);
            bottomBoardRect.DOAnchorPosY(-100, 0f);
        }).
        Append(topBoardRect.DOAnchorPosY(0, 0.65f).SetEase(Ease.OutExpo)).
        Join((bottomBoardRect.DOAnchorPosY(0, 0.65f).SetEase(Ease.OutExpo)))
        .SetUpdate(true);
    }

    // On -> Off Ʈ���� (Board)
    public void OffBoard()
    {
        // ��Ʈ�� ������ ���۽� ������ ų (����)
        sequence.Kill();
        // ��Ʈ�� ������ �ʱ⼳��
        sequence = DOTween.Sequence().OnStart(() =>
        {
            topBoardRect.DOAnchorPosY(0, 0f);
            bottomBoardRect.DOAnchorPosY(0, 0f);
        }).
        Append(topBoardRect.DOAnchorPosY(100, 0.65f).SetEase(Ease.OutExpo)).
        Join((bottomBoardRect.DOAnchorPosY(-100, 0.65f).SetEase(Ease.OutExpo)))
        .SetUpdate(true);
    }

    // Board �ؽ�Ʈ ����
    public void SetBoardText(string _topText, string _escText)
    {
        topText.text = _topText; 
        escText.text = _escText;
    }

    // Ÿ����
    public IEnumerator Typing(TextMeshProUGUI tmpText, string message, float duration)
    {
    
        tmpText.text = message;
        TypingTextEffect(tmpText, duration);

        yield return null; 
    }

    // TMP Ÿ���� ����Ʈ
    public static void TypingTextEffect(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To( x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration)
            .SetUpdate(true);
    }
}
