using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Vector2Int PlayerSpawnpointCell;
    [SerializeField]
    private GameObject PlayerPrefab;

    [SerializeField]
    private float SpawnDelay;
    [SerializeField]
    private int MaxFarmersCount;
    private int farmersCount;
    [SerializeField]
    private GameObject FarmerPrefab;

    private void Start()
    {
        SpawnPlayer();
        farmersCount = 0;
        SpawnFarmer();
    }

    private void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, GameManager.Instance.Grid.CellToWorldPos(PlayerSpawnpointCell), Quaternion.identity);
    }

    private void SpawnFarmer()
    {
        Vector2Int rnd;
        do
        {
            rnd = new Vector2Int(Random.Range(0, GameManager.Instance.Grid.Lines), Random.Range(GameManager.Instance.Grid.Columns/ 2,
                GameManager.Instance.Grid.Columns));
        } while (!GameManager.Instance.Grid.IsFree(rnd) || (GameManager.Instance.Player != null
        && GameManager.Instance.Grid.GetCellObject(rnd) == GameManager.Instance.Player.gameObject));

            Instantiate(FarmerPrefab, GameManager.Instance.Grid.CellToWorldPos(rnd), Quaternion.identity);
        farmersCount++;

        StartCoroutine(WaitingNextSpawn());
    }

    private IEnumerator WaitingNextSpawn()
    {
        do
        {
            yield return new WaitForSeconds(SpawnDelay);
        } while (!CanSpawn());

        SpawnFarmer();
    }

    private bool CanSpawn()
    {
        if(farmersCount < MaxFarmersCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeadFarmer()
    {
        farmersCount--;
    }
}
