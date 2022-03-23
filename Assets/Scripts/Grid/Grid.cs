using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int Columns;        //count of columns at map
    [SerializeField]
    private int Lines;          //count of lines at map
    [SerializeField]
    private float OffsetX;
    [SerializeField]
    private float OffsetY;
    [SerializeField]
    private Vector2 CellSize;   //cell size
    [SerializeField]
    private float DeltaX;       //X-axis difference between cells in the same column

    private GameObject[,] Map;

    [SerializeField]
    private GameObject StonePrefab;

    private void Awake()
    {
        InitMap();
        SpawnStones();
        GameManagerSet();
    }

    private void GameManagerSet()
    {
        GameManager.Instance.Grid = this;
    }

    private void InitMap()          //allocate memory for map
    {
        Map = new GameObject[Lines, Columns];
    }

    private void SpawnStones()      //spawn stones every second cell
    {
        for (int i = 1; i < Lines; i += 2)
        {
            for (int j = 1; j < Columns; j += 2)
            {
                GameObject stone = Instantiate(StonePrefab, CellToWorldPos(i, j), Quaternion.identity);
                Map[i, j] = stone;
            }
        }
    }

    public Vector2Int WorldToCellPos(Vector2 worldPos)       //getting map[i][j] pos like a Vector2, from global pos
    {
        for (int i = 0; i < Lines; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Vector2 cellPos = CellToWorldPos(i, j);
                if (worldPos.x < cellPos.x + CellSize.x / 2f && worldPos.y < cellPos.y + CellSize.y / 2f
                    && worldPos.x > cellPos.x - CellSize.x / 2f && worldPos.y > cellPos.y - CellSize.y / 2f)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);      //if world pos out of bounds
    }

    public Vector2 CellToWorldPos(int line, int column)      //getting center of map[i][j] cell's, in global pos like vector2
    {
        return new Vector2(column * CellSize.x + CellSize.x / 2f - line * DeltaX + OffsetX,
            line * -CellSize.y - CellSize.y / 2f + OffsetY);
    }

    public Vector2 CellToWorldPos(Vector2 cell)      //getting center of map[cell.x][cell.y] cell's, in global pos like vector2
    {
        return new Vector2(cell.y * CellSize.x + CellSize.x / 2f - cell.x * DeltaX + OffsetX,
            cell.x * -CellSize.y - CellSize.y / 2f + OffsetY);
    }

    public bool IsFree(int line, int column)
    {
        if (line < Lines && column < Columns && line > -1 && column > -1)
        {
            if (Map[line, column] == null)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsFree(Vector2Int cell)
    {
        if (cell.x < Lines && cell.y < Columns && cell.x > -1 && cell.y > -1)
        {
            if (Map[cell.x, cell.y] == null)
            {
                return true;
            }
        }

        return false;
    }
}
