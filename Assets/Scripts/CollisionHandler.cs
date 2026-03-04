using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                LevelFinished();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    private void LevelFinished()
    {
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke("LoadNextLevel", delay);
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        Invoke("ReloadLevel", delay);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }

    }
}
