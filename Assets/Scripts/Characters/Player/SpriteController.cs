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
        chrState = State.Clear;
        lookDir = LookDir.Right;
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
