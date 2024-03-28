using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabCharacterUi : MonoBehaviour, IUiEffectPos
{
    [field: Header("Sprites")]
    [field: SerializeField] private Sprite idleSprite { get; set; }
    [field: SerializeField] private Sprite blinkSprite { get; set; }

    [field: Header("Ui Effect")]
    [field: SerializeField] public Vector2 normalPos { get; set; }
    [field: SerializeField] public Vector2 effectPos { get; set; }
    [field: SerializeField] public Vector2 effectScale { get; set; }

    Vector3 normalVector3 = new Vector3(0.75f, 0.75f, 0.75f);
    Image image;

    private void OnEnable()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }

        OnIdle();
    }

    private void OnBlink()
    {
        image.sprite = blinkSprite;
        image.transform.DOScale(normalVector3, 0.2f).OnComplete(() => OnIdle())
            .SetUpdate(true);
    }

    private void OnIdle()
    {
        image.sprite = idleSprite;
        image.transform.DOScale(normalVector3, 3.2f).OnComplete(() => OnBlink())
            .SetUpdate(true);
    }

    private void OnDisable()
    {
        image.transform.DOKill();
    }
}
