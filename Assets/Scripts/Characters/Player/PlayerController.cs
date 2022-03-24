using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : CharacterController
{
    private Joystick joystick;

    [SerializeField]
    private GameObject BombPrefab;
    [SerializeField]
    private float ReloadTime;
    private bool canSpawn;

    protected override void Start()
    {
        InitHealthBar();

        base.Start();

        GameManagerSet();

        InitJoystick();

        InitAttack();
    }

    private void InitHealthBar()
    {
        HealthBar = GameManager.Instance.PlayerHealthBar;
    }

    private void GameManagerSet()
    {
        GameManager.Instance.Player = this;
    }

    private void InitJoystick()
    {
        joystick = GameManager.Instance.Joystick;
    }

    private void InitAttack()
    {
        GameManager.Instance.BombBackground.GetComponent<Button>().onClick.AddListener(Attack);
        GameManager.Instance.BombBackground.color = Color.white;
        GameManager.Instance.BombImage.color = Color.white;
        GameManager.Instance.BombImage.fillAmount = 1;
        canSpawn = true;
    }

    private void Update()
    {
        if (joystick.Vertical > 0 && Mathf.Abs(joystick.Vertical) > Mathf.Abs(joystick.Horizontal))
        {
            TryMove(Vector2Int.left);
        }
        else if (joystick.Vertical < 0 && Mathf.Abs(joystick.Vertical) > Mathf.Abs(joystick.Horizontal))
        {
            TryMove(Vector2Int.right);
        }
        else if (joystick.Horizontal < 0 && Mathf.Abs(joystick.Horizontal) > Mathf.Abs(joystick.Vertical))
        {
            TryMove(Vector2Int.down);
        }
        else if (joystick.Horizontal > 0 && Mathf.Abs(joystick.Horizontal) > Mathf.Abs(joystick.Vertical))
        {
            TryMove(Vector2Int.up);
        }
    }

    private void Attack()
    {
        if(canSpawn)
        {
            Instantiate(BombPrefab, GameManager.Instance.Grid.CellToWorldPos(GameManager.Instance.Grid.WorldToCellPos(transform.position)),
                Quaternion.identity);
            canSpawn = false;
            StartCoroutine(Reloading());
        }
    }

    private IEnumerator Reloading()
    {
        GameManager.Instance.BombBackground.color = new Color(GameManager.Instance.BombBackground.color.r,
            GameManager.Instance.BombBackground.color.g, GameManager.Instance.BombBackground.color.b, 0.5f);

        GameManager.Instance.BombImage.color = new Color(GameManager.Instance.BombImage.color.r,
            GameManager.Instance.BombImage.color.g, GameManager.Instance.BombImage.color.b, 0.5f);

        GameManager.Instance.BombImage.fillAmount = 0;

        for (int i = 0; i < 100; i++)
        {
            GameManager.Instance.BombImage.fillAmount = (i + 1) / 100f;
            yield return new WaitForSeconds(ReloadTime / 100);
        }

        GameManager.Instance.BombBackground.color = Color.white;
        GameManager.Instance.BombImage.color = Color.white;
        canSpawn = true;
    }

    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.GetComponent<EnemyController>())
        {
            TakeDamage(collision.GetComponent<EnemyController>().GetDamage());
        }
    }
}
