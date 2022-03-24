using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    public PlayerController Player;
    [SerializeField]
    public Grid Grid;
    [SerializeField]
    public Spawner Spawner;
    [SerializeField]
    public HealthBar PlayerHealthBar;
    [SerializeField]
    public Joystick Joystick;
    [SerializeField]
    public Image BombBackground;
    [SerializeField]
    public Image BombImage;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
