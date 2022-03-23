using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteController))]
public abstract class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float MoveDuration;
    Tweener posTweener;

    protected virtual void Start()
    {
        InitTweener();
    }

    private void InitTweener()
    {
        posTweener = transform.DORotate(Vector3.zero, 0);
    }

    protected void TryMove(Vector2Int directrion)
    {
        if (!posTweener.IsPlaying())
        {
            if (GameManager.Instance.Grid.IsFree(GameManager.Instance.Grid.WorldToCellPos(transform.position) + directrion))
            {
                posTweener = transform.DOMove(GameManager.Instance.Grid.CellToWorldPos(GameManager.Instance.Grid.WorldToCellPos(transform.position) + directrion),
                    MoveDuration);
                posTweener.SetEase(Ease.Linear);
            }
        }
    }
}
