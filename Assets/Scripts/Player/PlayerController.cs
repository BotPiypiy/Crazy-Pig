using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float MoveDuration;

    [SerializeField]
    SpriteController SpriteController;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SpriteController.LookTo(LookDir.Up);
            TryMove(Vector2Int.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpriteController.LookTo(LookDir.Down);
            TryMove(Vector2Int.right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpriteController.LookTo(LookDir.Left);
            TryMove(Vector2Int.down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpriteController.LookTo(LookDir.Right);
            TryMove(Vector2Int.up);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    private void TryMove(Vector2Int directrion)
    {
        if(Grid.IsFree(Grid.WorldToCellPos(transform.position) + directrion))
        {
            transform.DOMove(Grid.CellToWorldPos(Grid.WorldToCellPos(transform.position) + directrion), MoveDuration);
        }
    }
}
