﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    [SerializeField] string levelToLoad = "";
    [SerializeField] bool selectRandomScene = true;

    PlayerHealthManager playerHealthManager;
    AudioManager audioManager;

    List<string> scenesList = new List<string>(); //string list of levels to randomly select from

    void Start()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        audioManager = FindObjectOfType<AudioManager>();

        scenesList.Add("TestScene1"); //on start, manually adds a level name to the scenesList list
        scenesList.Add("TestScene2");
        scenesList.Add("TestScene3");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //checks if the player was the object that collided with the gameobject the script is attached to
        {
            if (selectRandomScene) //if the selectRandomScene boolean is active, then the gameobject the script is attached to wants to randomly load levels from a list
            {
                if (scenesList.Count > 0) //if the array doesn't have any levels to select from, the code will not be run to avoid any errors
                {
                    int randomSceneNum = Random.Range(0, scenesList.Count); //randomly selects a number to use to select from the list
                    //SceneManager.LoadScene(scenesList[randomSceneNum]); //actually loads a scene
                    Debug.Log("Load " + scenesList[randomSceneNum]); //logs that the level swap would have been successful
                    scenesList.Remove(scenesList[randomSceneNum]);
                }
            }
            else
            {
                SceneManager.LoadScene(levelToLoad); //loads a selected scene since random selection is not the desired functionality
            }
        }
    }

    public void LoadEndGameScene(string sceneToLoad)
    {
        StartCoroutine(DeathTime(sceneToLoad));
    }

    IEnumerator DeathTime(string sceneToLoad) //has the player wait a few seconds before GAME OVER is displayed in order to give time for any death animations/sounds
    {
        audioManager.PlayAudio(audioManager.GetDeathSound()); //NOT playing the audio for some reason
        yield return new WaitForSeconds(3); //plays for the length of the audio clip
        SceneManager.LoadScene(sceneToLoad);
    }
}