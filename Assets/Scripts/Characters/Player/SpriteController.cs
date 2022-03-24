using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteController : MonoBehaviour
{
    [SerializeField]
    Sprite[] Up;
    [SerializeField]
    Sprite[] Right;
    [SerializeField]
    Sprite[] Down;
    [SerializeField]
    Sprite[] Left;

    [SerializeField]
    SpriteRenderer SpriteRenderer;

    private State chrState;
    private LookDir lookDir;


    private void Start()
    {
        InitStates();
        InitOrder();
    }

    private void InitStates()
    {
        chrState = State.Clear;
        lookDir = LookDir.Right;
    }

    private void InitOrder()
    {
        SpriteRenderer.sortingOrder = GameManager.Instance.Grid.WorldToCellPos(transform.position).x;
    }

    public void LookTo(LookDir dir)
    {
        lookDir = dir;

        switch (lookDir)
        {
            case LookDir.Up:
                {
                    SpriteRenderer.sprite = Up[(int)chrState];
                    break;
                }
            case LookDir.Right:
                {
                    SpriteRenderer.sprite = Right[(int)chrState];
                    break;
                }
            case LookDir.Down:
                {
                    SpriteRenderer.sprite = Down[(int)chrState];
                    break;
                }
            case LookDir.Left:
                {
                    SpriteRenderer.sprite = Left[(int)chrState];
                    break;
                }
            default:
                {
                    SpriteRenderer.sprite = Right[(int)chrState];
                    break;
                }
        }
    }

    public void ChangeState(State state)
    {
        chrState = state;
        LookTo(lookDir);
    }

    public void SetOrderInLayer(int value)
    {
        SpriteRenderer.sortingOrder = value;
    }

    public void AddOrderInLayer()
    {
        SpriteRenderer.sortingOrder++;
    }

    public void SubOrderInLayer()
    {
        SpriteRenderer.sortingOrder--;
    }

    public void IncreaseOrderInLayer()
    {
        SpriteRenderer.sortingOrder++;
    }

    public void Decrease9OrderInLayer()
    {
        SpriteRenderer.sortingOrder--;
    }
}

public enum LookDir
{
    Up,
    Right,
    Down,
    Left
}

public enum State
{
    Clear,
    Angry,
    Dirty
}
