﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] int enemyMaxHealth;
    [SerializeField] int enemyCurrentHealth;

    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject); //destroys the gameobject the script is attached to
        }
    }

    public void HurtEnemy(int damageToDeal)
    {
        enemyCurrentHealth -= damageToDeal;
    }

    public int GetMaxHealth()
    {
        return enemyMaxHealth;
    }

    public void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    public int GetCurrentHealth()
    {
        return enemyCurrentHealth;
    }

    public void SetCurrentHealth(int updatedCurrentEnemyHealth)
    {
        enemyCurrentHealth = updatedCurrentEnemyHealth;
    }
}
