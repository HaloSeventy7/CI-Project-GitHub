﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject entryToDestroy; //optional
    [SerializeField] bool multipleKeysRequired = false;
    [SerializeField] AudioClip soundToPlay;

    AudioManager audioManager;
    MultiKeyLock multiKeyLock;

    void Start()
    {
        multiKeyLock = FindObjectOfType<MultiKeyLock>();
    }

    void Update()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!multipleKeysRequired && entryToDestroy != null) //if multiple keys aren't required to destroy the entry, then simply destroy the entry now
            {
                if (soundToPlay != null)
                {
                    audioManager.PlaySoundFXAudio(soundToPlay);
                }
                Destroy(entryToDestroy);
                Destroy(gameObject); //destroy this key
            }
            else
            {
                multiKeyLock.UpdateKeysRequired(gameObject); //tests if the required keys array list needs to be updated
                Destroy(gameObject); //destroy this key
            } 
        }
    }
}
