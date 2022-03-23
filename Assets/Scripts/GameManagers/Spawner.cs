using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Vector2Int PlayerSpawnpointCell;
    [SerializeField]
    private GameObject PlayerPrefab;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, Grid.CellToWorldPos(PlayerSpawnpointCell), Quaternion.identity);
    }
}
