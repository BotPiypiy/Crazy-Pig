using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionPrefab;
    [SerializeField]
    private float TimeBeforeExplosion;
    [SerializeField]
    private int ExplosionRadius;

    [SerializeField]
    private Vector2Int[] Directions;

    private void Start()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(TimeBeforeExplosion);
        Instantiate(ExplosionPrefab, GameManager.Instance.Grid.CellToWorldPos(GameManager.Instance.Grid.WorldToCellPos(transform.position)),
            Quaternion.identity);
        for (int i = 0; i < Directions.Length; i++)
        {
            for (int j = 1; j < ExplosionRadius; j++)
            {
                if (GameManager.Instance.Grid.IsFree(GameManager.Instance.Grid.WorldToCellPos(transform.position) + Directions[i] * j))
                {
                    Instantiate(ExplosionPrefab, GameManager.Instance.Grid.CellToWorldPos(GameManager.Instance.Grid.WorldToCellPos(transform.position)
                    + Directions[i] * j), Quaternion.identity);
                }
                else
                {
                    break;
                }
            }
        }

        Destroy(gameObject);
    }
}
