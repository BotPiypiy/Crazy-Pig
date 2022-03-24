using UnityEngine;

interface IEnemyController
{
    abstract Vector2Int ChooseDir();
}

public abstract class EnemyController : CharacterController
{
    [SerializeField]
    protected Vector2Int[] Directions;

    [SerializeField]
    protected int Damage;
    [SerializeField]
    protected float DieTime;

    [SerializeField]
    private int Score;

    protected int[] randomMap;
    protected bool isAlive;

    protected override void Start()
    {
        base.Start();

        InitMap();
        isAlive = true;
    }

    private void InitMap()
    {
        randomMap = new int[Directions.Length];
        for (int i = 0; i < Directions.Length; i++)
        {
            randomMap[i] = i;
        }
    }

    protected override void Die()
    {
        isAlive = false;
        SpriteController.ChangeState(State.Dirty);
        Destroy(gameObject, DieTime);
    }

    public int GetDamage()
    {
        return Damage;
    }
}
