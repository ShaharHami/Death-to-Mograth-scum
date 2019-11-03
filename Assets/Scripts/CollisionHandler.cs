using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("in seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;
    [SerializeField] GameObject playerRig;
    [SerializeField] GameObject forceField;
    [SerializeField] GameObject endUI;
    [SerializeField] float forceFieldDuration = 3f;
    [SerializeField] float explosionForce = 5000f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeStrength = 2f;
    
    int lives = 3;
    MusicPlayer musicPlayer;
    AudioSource music;
    LivesCounter livesCounter;
    bool invulnerable = false;
    Vector3 positionPlaceHolder;
    bool doShake = false;
    void Start()
    {
        livesCounter = FindObjectOfType<LivesCounter>();
        lives = livesCounter.lives;
    }
    void OnTriggerEnter(Collider coll)
    {
        if (!invulnerable)
        {
            livesCounter.UpdateLife(-1);
            lives = livesCounter.lives;
            if (lives <= 0)
            {
                StartDeathSequence();
            }
            else
            {
                StartDamageSequence();
            }
        }
    }
    void FixedUpdate()
    {
        if (doShake)
        {
            Vector3 offset = Random.insideUnitSphere;
            transform.localPosition = positionPlaceHolder + (offset * shakeStrength);
        }
    }
    void StartDamageSequence()
    {
        positionPlaceHolder = transform.localPosition;
        invulnerable = true;
        forceField.SetActive(true);
        doShake = true;
        Invoke("DeactivateForceField", forceFieldDuration);
        Invoke("DeactivateShake", shakeDuration);
    }
    void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        playerRig.GetComponent<Animator>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        musicPlayer = FindObjectOfType<MusicPlayer>();
        music = musicPlayer.GetComponent<AudioSource>();
        music.Pause();
        ExplodePlayer();
        Invoke("StopFX", levelLoadDelay);
    }
    private void StopFX() // string refernce
    {
        // GameObject endUI = GameObject.FindGameObjectWithTag("EndGameMenu");
        endUI.SetActive(true);
        deathFX.SetActive(false);
    }
    private void ExplodePlayer()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        GameObject[] parts = GameObject.FindGameObjectsWithTag("PlayerParts");
        foreach (GameObject part in parts)
        {
            part.AddComponent<BoxCollider>();
            part.AddComponent<Rigidbody>();
            Rigidbody rb = part.GetComponent<Rigidbody>();
            rb.mass = 5;
            rb.AddExplosionForce(explosionForce, deathFX.transform.position, explosionRadius);
        }
    }
    void DeactivateForceField()
    {
        forceField.SetActive(false);
        invulnerable = false;
    }
    void DeactivateShake()
    {
        doShake = false;
        transform.localPosition = positionPlaceHolder;
    }
}
