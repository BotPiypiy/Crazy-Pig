using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;

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
        CreateSingleton();
        InitMap();
        SpawnStones();
    }

    private void CreateSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    public static Vector2Int WorldToCellPos(Vector2 worldPos)       //getting map[i][j] pos like a Vector2, from global pos
    {
        for (int i = 0; i < Instance.Lines; i++)
        {
            for (int j = 0; j < Instance.Columns; j++)
            {
                Vector2 cellPos = CellToWorldPos(i, j);
                if (worldPos.x < cellPos.x + Instance.CellSize.x / 2f && worldPos.y < cellPos.y + Instance.CellSize.y / 2f
                    && worldPos.x > cellPos.x - Instance.CellSize.x / 2f && worldPos.y > cellPos.y - Instance.CellSize.y / 2f)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);      //if world pos out of bounds
    }

    public static Vector2 CellToWorldPos(int line, int column)      //getting center of map[i][j] cell's, in global pos like vector2
    {
        return new Vector2(column * Instance.CellSize.x + Instance.CellSize.x / 2f - line * Instance.DeltaX + Instance.OffsetX,
            line * -Instance.CellSize.y - Instance.CellSize.y / 2f + Instance.OffsetY);
    }

    public static Vector2 CellToWorldPos(Vector2 cell)      //getting center of map[cell.x][cell.y] cell's, in global pos like vector2
    {
        return new Vector2(cell.y * Instance.CellSize.x + Instance.CellSize.x / 2f - cell.x * Instance.DeltaX + Instance.OffsetX,
            cell.x * -Instance.CellSize.y - Instance.CellSize.y / 2f + Instance.OffsetY);
    }

    public static bool IsFree(int line, int column)
    {
        if (line < Instance.Lines && column < Instance.Columns && line > -1 && column > -1)
        {
            if (Instance.Map[line, column] == null)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsFree(Vector2Int cell)
    {
        if (cell.x < Instance.Lines && cell.y < Instance.Columns && cell.x > -1 && cell.y > -1)
        {
            if (Instance.Map[cell.x, cell.y] == null)
            {
                return true;
            }
        }

        return false;
    }
}
