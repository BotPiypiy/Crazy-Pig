using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : EnemyController, IEnemyController
{
    private void Update()
    {
        if (isAlive)
        {
            TryMove(ChooseDir());
        }
    }

    public Vector2Int ChooseDir()
    {
        GameObject player = GameManager.Instance.Player.gameObject;
        Grid grid = GameManager.Instance.Grid;

        for (int i = 0; i < Directions.Length; i++)
        {
            if (grid.CheckBounds(grid.WorldToCellPos(transform.position) + Directions[i]))
            {
                if (grid.GetCellObject(grid.WorldToCellPos(transform.position) + Directions[i]) == player)
                {
                    SpriteController.ChangeState(State.Angry);
                    return Directions[i];
                }
            }
        }

        ShuffleArray(randomMap);

        for (int i = 0; i < Directions.Length; i++)
        {
            if (grid.CheckBounds(grid.WorldToCellPos(transform.position) + Directions[i]))
            {
                if (grid.GetCellObject(grid.WorldToCellPos(transform.position) + Directions[i]) == null)
                {
                    SpriteController.ChangeState(State.Clear);
                    return Directions[randomMap[i]];
                }
            }
        }

        return Vector2Int.zero;
    }

    private void ShuffleArray(int[] col)
    {
        int n = col.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            int temp = col[n];
            col[n] = col[k];
            col[k] = temp;
        }
    }

    protected override void Die()
    {
        GameManager.Instance.Spawner.DeadFarmer();
        base.Die();
    }
}
