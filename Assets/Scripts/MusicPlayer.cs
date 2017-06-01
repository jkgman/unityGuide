using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _music;
    [SerializeField] private AudioSource _musicSource;

    void Start()
    {
        _musicSource = GetComponent<AudioSource>();
        PlayMusic();
    }


    public void PlayMusic()
    {
        _musicSource.clip = _music;
        _musicSource.Play();
    }
}