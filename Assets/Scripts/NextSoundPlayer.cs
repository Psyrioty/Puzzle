using System.Collections.Generic;
using UnityEngine;

public class NextSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private int currentTrack; //итератор музык

    [SerializeField] private List<AudioClip> sounds;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }



    //---------------------ЗВУКИ---------------------------------

    //Музыка
    private void PlayNextTrack()
    {
        if (sounds.Count == 0)
            return;

        audioSource.clip = sounds[currentTrack];
        audioSource.Play();

        currentTrack++;

        if (currentTrack >= sounds.Count)
        {
            currentTrack = 0;
        }
    }
    //==============================================================
}
