﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarManager : MonoBehaviour
{
    public Slider healthBar;
    public Text healthText;

    private int maxHealth;
    private int currentHealth; //both these variables are set to floats so they don't round to "0" when calculating the player's current health

    PlayerHealthManager playerHealthManager;
    CharacterSelector characterSelector;

    void Start()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>(); //get a reference to the PlayerHealthManager script
        characterSelector = FindObjectOfType<CharacterSelector>(); //get a reference to the CharacterSelector script
    }

    void Update()
    {
        if (characterSelector.GetCharacterActive()) //checks if the character is active
        {
            playerHealthManager = characterSelector.GetCharacterObject().GetComponent<PlayerHealthManager>(); //get the instantiated (or active) player's PlayerHealthManager script
            maxHealth = playerHealthManager.GetMaxHealth(); //sets a variable to teh value of the character's max health
            currentHealth = playerHealthManager.GetCurrentHealth(); //sets a variable to teh value of the character's max health
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            //healthText.text = "HP: " + ((currentHealth / maxHealth) * 100).ToString() + "%"; //sets the health text to the player's current health as a percentage
            healthText.text = "HP: " + (currentHealth.ToString() + " / " + maxHealth.ToString()); //sets the health text to the player's current health over max health //changed to avoid decimal percentages (since the Warrior and Archer's health isn't 100)
        }
    }
}