using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField]
    SpriteController SpriteController;

    protected override void Start()
    {
        base.Start();

        GameManagerSet();
    }

    private void GameManagerSet()
    {
        GameManager.Instance.Player = this;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            SpriteController.LookTo(LookDir.Up);
            TryMove(Vector2Int.left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            SpriteController.LookTo(LookDir.Down);
            TryMove(Vector2Int.right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            SpriteController.LookTo(LookDir.Left);
            TryMove(Vector2Int.down);
        }
        if (Input.GetKey(KeyCode.D))
        {
            SpriteController.LookTo(LookDir.Right);
            TryMove(Vector2Int.up);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}
