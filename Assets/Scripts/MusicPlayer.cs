using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        int musicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        AudioSource player = GetComponent<AudioSource>();
        if (!player.isPlaying)
        {
            player.Play();
        }
    }
}
