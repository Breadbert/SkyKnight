using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name;

    public EnemyStats enemyStats;
    public int HP;
    private int MaxHP;
    public Stat Damage;
    public Stat Defence;

    void Start()
    {
        MaxHP = HP;
    }

    void Update()
    {

    }
    
}
[System.Serializable]
public class EnemyStats
{
    public int Damage;
    public int Health;
    public int Defence;
}