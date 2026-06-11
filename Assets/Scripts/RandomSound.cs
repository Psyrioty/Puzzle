using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;




    //проигрывание звука при старте перемещения
    public void PlaySoundRandom(List<AudioClip> sounds)
    {
        CheckAudioSource();


        if(audioSource == null)
        {
            return;
        }

        if (!audioSource.enabled)
        {
            return;
        }

        if (sounds.Count == 0)
        {
            return;
        }
        

        AudioClip clip = sounds[Random.Range(0, sounds.Count)];

        audioSource.PlayOneShot(clip);
    }


    //проигрывание звука при старте перемещения
    public void PlaySoundRandom(List<AudioClip> sounds, AudioSource audioSource)
    {
        if(audioSource == null)
        {
            return;
        }

        if (sounds.Count == 0)
        {
            return;
        }
        

        AudioClip clip = sounds[Random.Range(0, sounds.Count)];

        audioSource.PlayOneShot(clip);
    }

    private void CheckAudioSource()
    {
        if(audioSource != null)
        {
            return;
        }
        audioSource = GetComponent<AudioSource>();
    }
}
