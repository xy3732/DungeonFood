using DG.Tweening;
using UnityEngine;

public class HaloEffect : MonoBehaviour, IUiEffectPos
{
    [field: SerializeField] private float haloY { get; set; }
    private float sinY { get; set; }
    private float runtime { get; set; }

    private RectTransform rect { get; set; }
    
    [field : Header("Ui Effect")]
    [field: SerializeField] public Vector2 normalPos { get; set; }
    [field: SerializeField] public Vector2 effectPos { get; set; }
    [field: SerializeField] public Vector2 effectScale { get; set; }

    private void OnEnable()
    {
        if (rect == null)
        {
            rect = gameObject.GetComponent<RectTransform>();
        }

        sinY = 0;
        runtime = 0;

    }

    private void Update()
    {
        runtime += Time.fixedDeltaTime * 1f;

        sinY = 5f * Mathf.Sin(runtime);

        rect.DOAnchorPosY(haloY + sinY, 0f).SetUpdate(true);
    }

    private void OnDisable()
    {
        rect.DOKill();
    }
}
