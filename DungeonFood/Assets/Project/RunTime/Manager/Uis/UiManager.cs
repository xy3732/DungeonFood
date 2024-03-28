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

    // Ui Manager 리스트
    public static CutEffectManager cutEffect;
    public static TabManager tabManager;

    // Bool
    [field: HideInInspector] public bool onTab { get; set; }
    [field: HideInInspector] public bool firstIn { get; set; }

    //Action
    [field: HideInInspector] public Action offUi { get; set; }
    [field: HideInInspector] public Action previousUi { get; set; }

    // 닷트윈 시퀸스
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

        // Tab Ui 활성화 되있으면 해당 Ui 종료후 게임으로 돌아가기
        if(onTab)
        {
            ToGameUi();
        }
        // 아니면 일시정지 Ui 띄우기
        else
        {

        }
    }

    // Tab 이펙트
    private void Tab()
    {
        // 강제 코루틴 종료
        StopCoroutine(PostProcessManager.instance.BlurLerp(1, 1, 0f));

        onTab = !onTab;

        // tab ui 이펙트 및 오브젝트 - FALSE
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
        // tab ui 이펙트 및 오브젝트 - TRUE
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

    // 메인 ui 컨트롤 (active)
    private void SetMainUiActive(bool set)
    {
        mainUis.SetActive(set);
    }

    // Ui 오브젝트 활성화 설정 부분
    private void SetUiObject(bool _active)
    {
        MapTextUI.gameObject.SetActive(_active);
    }

    // 씬 로드시 오브젝트 삭제 방지
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

    // Off -> On 트위닝 (Board)
    public void OnBoard()
    {
        // 닷트윈 시퀸스 시작시 강제로 킬 (종료)
        sequence.Kill();
        // 닷트윈 시퀸스 초기설정
        sequence = DOTween.Sequence().OnStart(() =>
        {
            topBoardRect.DOAnchorPosY(100, 0f);
            bottomBoardRect.DOAnchorPosY(-100, 0f);
        }).
        Append(topBoardRect.DOAnchorPosY(0, 0.65f).SetEase(Ease.OutExpo)).
        Join((bottomBoardRect.DOAnchorPosY(0, 0.65f).SetEase(Ease.OutExpo)))
        .SetUpdate(true);
    }

    // On -> Off 트위닝 (Board)
    public void OffBoard()
    {
        // 닷트윈 시퀸스 시작시 강제로 킬 (종료)
        sequence.Kill();
        // 닷트윈 시퀸스 초기설정
        sequence = DOTween.Sequence().OnStart(() =>
        {
            topBoardRect.DOAnchorPosY(0, 0f);
            bottomBoardRect.DOAnchorPosY(0, 0f);
        }).
        Append(topBoardRect.DOAnchorPosY(100, 0.65f).SetEase(Ease.OutExpo)).
        Join((bottomBoardRect.DOAnchorPosY(-100, 0.65f).SetEase(Ease.OutExpo)))
        .SetUpdate(true);
    }

    // Board 텍스트 설정
    public void SetBoardText(string _topText, string _escText)
    {
        topText.text = _topText; 
        escText.text = _escText;
    }

    // 타이핑
    public IEnumerator Typing(TextMeshProUGUI tmpText, string message, float duration)
    {
    
        tmpText.text = message;
        TypingTextEffect(tmpText, duration);

        yield return null; 
    }

    // TMP 타이핑 이펙트
    public static void TypingTextEffect(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To( x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration)
            .SetUpdate(true);
    }
}
