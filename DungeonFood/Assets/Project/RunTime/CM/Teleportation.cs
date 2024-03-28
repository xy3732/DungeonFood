using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using GamePix.Ui;

public class Teleportation : MonoBehaviour
{
    [field: Header("To Move")]
    [field: SerializeField] public Transform toMove { get; set; }

    [field: Header("CutMove")]
    [field: SerializeField] public UiWay3 moveStart { get; set; }
    [field: SerializeField] public UiWay3 moveEnd { get; set; }

    private Vector3 transStart;
    private Vector3 transEnd;
    private Sequence sequence;

    private void Start()
    {
        switch (moveStart)
        {
            case UiWay3.Top:
                transStart = UiVector3.Top;
                break;

            case UiWay3.Down:
                transStart = UiVector3.Down;
                break;

            case UiWay3.Left:
                transStart = UiVector3.Left;
                break;

            case UiWay3.Right:
                transStart = UiVector3.Right;
                break;

            case UiWay3.Center:
                transStart = UiVector3.Center;
                break;
        }

        switch (moveEnd)
        {
            case UiWay3.Top:
                transEnd = UiVector3.Top;
                break;

            case UiWay3.Down:
                transEnd= UiVector3.Down;
                break;

            case UiWay3.Left:
                transEnd= UiVector3.Left;
                break;

            case UiWay3.Right:
                transEnd = UiVector3.Right;
                break;

            case UiWay3.Center:
                transEnd = UiVector3.Center;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        FromEffect(other);
    }

    private void FromEffect(Collider2D other)
    {
        sequence.Kill();
        sequence = DOTween.Sequence()
            .OnStart(() => { UiManager.cutEffect.CenterFrom(transStart, Ease.OutCirc); })
            .Append(gameObject.transform.DOScale(Vector3.one, 0.6f))
            .OnComplete(() => 
            {
                other.transform.position = toMove.position;
                GameManager.instance.vCamMouseTarget.transform.position = toMove.position;

                UiManager.cutEffect.CenterTo(transEnd, Ease.InCirc); 
            });
    }
}
