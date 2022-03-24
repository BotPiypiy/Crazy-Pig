using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private int Damage;
    [SerializeField]
    private float LiveTime;

    private void Start()
    {
        Destroy(gameObject, LiveTime);
    }

    public int GetDamage()
    {
        return Damage;
    }
}
