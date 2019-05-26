﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] int healthToReturn = 10;

    PlayerHealthManager playerHealthManager;
    CharacterSelector characterSelector;

    private int maxHealth;
    private int currentHealth;

    void Update()
    {
        characterSelector = FindObjectOfType<CharacterSelector>();
        if (characterSelector.GetCharacterActive()) //checks if the character is active
        {
            playerHealthManager = characterSelector.GetCharacterObject().GetComponent<PlayerHealthManager>(); //gets the current player instance's health manager
            maxHealth = playerHealthManager.GetMaxHealth();
            currentHealth = playerHealthManager.GetCurrentHealth();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealthManager.SetCurrentHealth(currentHealth + healthToReturn); //health is checked in PlayerHealthManager to ensure the value isn't larger than it's max health
            Destroy(gameObject);
        }
    }
}
