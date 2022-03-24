using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteController))]
public abstract class CharacterController : MonoBehaviour
{
    [SerializeField]
    private int MaxHp;
    private int Hp;
    [SerializeField]
    protected HealthBar HealthBar;

    [SerializeField]
    private float MoveDuration;
    Tweener posTweener;

    [SerializeField]
    protected SpriteController SpriteController;

    protected bool immunity;

    protected virtual void Start()
    {
        InitTweener();
        InitHp();
    }

    private void InitTweener()
    {
        posTweener = transform.DORotate(Vector3.zero, 0);
    }
    
    private void InitHp()
    {
        Hp = MaxHp;
        immunity = false;

        HealthBar.InitHealthBar(MaxHp);
    }

    protected void TryMove(Vector2Int directrion)
    {
        if (!posTweener.IsPlaying())
        {
            if (GameManager.Instance.Grid.IsFree(GameManager.Instance.Grid.WorldToCellPos(transform.position) + directrion))
            {
                if (directrion == Vector2Int.left)
                {
                    SpriteController.LookTo(LookDir.Up);
                    SpriteController.SubOrderInLayer();
                }
                else if (directrion == Vector2Int.up)
                {
                    SpriteController.LookTo(LookDir.Right);
                }
                else if (directrion == Vector2Int.right)
                {
                    SpriteController.LookTo(LookDir.Down);
                    SpriteController.AddOrderInLayer();
                }
                else if (directrion == Vector2Int.down)
                {
                    SpriteController.LookTo(LookDir.Left);
                }

                GameManager.Instance.Grid.SetCellObject(null, GameManager.Instance.Grid.WorldToCellPos(transform.position));

                Vector2 endPos = GameManager.Instance.Grid.CellToWorldPos(GameManager.Instance.Grid.WorldToCellPos(transform.position) + directrion);
                posTweener = transform.DOMove(endPos, MoveDuration);
                posTweener.SetEase(Ease.Linear);

                GameManager.Instance.Grid.SetCellObject(gameObject, GameManager.Instance.Grid.WorldToCellPos(transform.position) + directrion);
            }
        }
    }

    private IEnumerator SetImmunity()
    {
        immunity = true;
        yield return new WaitForSeconds(1f);
        immunity = false;
    }

    protected void TakeDamage(int damage)
    {
        if (!immunity)
        {
            Hp -= damage;
            HealthBar.SetHealth(Hp);

            if (Hp < 1)
            {
                Die();
            }
            StartCoroutine(SetImmunity());
        }
    }

    protected abstract void Die();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explosion>() != null)
        {
            TakeDamage(collision.GetComponent<Explosion>().GetDamage());
        }
    }
}
