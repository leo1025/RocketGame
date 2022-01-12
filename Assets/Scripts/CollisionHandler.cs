using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace before using it.

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successCheer;

    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) // other refers to object that was collided with
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default: // method here to reload scene
                StartCrashSequence();
                break;
        }
    }
    
    void StartSuccessSequence() 
    {
        // To remove control from player, can disable movement script.
        // TODO: add Particle Effects
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successCheer);
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence() 
    {
        // To remove control from player, can disable movement script.
        // TODO: add Particle Effects
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    // Make things easy to read.
    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // Get the current index of the scene that we're on
    }

    void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
