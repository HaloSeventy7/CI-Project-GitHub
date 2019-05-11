﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ironskin : MonoBehaviour
{

    [HideInInspector] public float moveSpeedMultiplier;
    [HideInInspector] public int shieldHealth;
    [HideInInspector] public float buffDuration = 5f;

    private int currentHealth;

    PlayerController playerController;
    PlayerHealthManager playerHealthManager;
    public void Setup() //performs the same actions as MonoBehaviour's Start() function
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
    }

    void Update()
    {
        currentHealth = playerHealthManager.GetCurrentHealth();
    }

    
    public void PerformIronskin()
    {
        StartCoroutine(BuffTimer()); //calls the coroutine defined below to activate the buff temporarily
    }

    IEnumerator BuffTimer() //coroutine that works as a timer
    {
        float storedMoveSpeed = playerController.moveSpeed; //locally stores the value of the player's current move speed before it gets altered
        int storedHealth = currentHealth; //locally stores the value of the player's health before it gets altered

        playerController.moveSpeed *= moveSpeedMultiplier; //increases the move speed in the PlayerController script
        playerHealthManager.SetCurrentHealth(currentHealth + shieldHealth);
        yield return new WaitForSeconds(buffDuration); //keeps buffs active for as long as the buff duration time

        playerController.moveSpeed = storedMoveSpeed; //sets the move speed of the player back to normal
        if (currentHealth > storedHealth)
        {
            currentHealth = storedHealth; 
        }
    }
}