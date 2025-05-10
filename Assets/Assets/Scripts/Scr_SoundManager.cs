using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SoundManager : MonoBehaviour
{
    public static Scr_SoundManager instance;

    [SerializeField] private AudioSource soundFXobject;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {

        // Spawn in GameObject
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);

        // Assing clip
        audioSource.clip = audioClip;

        // Assing volume
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Get length of sound
        float clipLength = audioSource.clip.length;

        // Destroy clip after done playing
        Destroy(audioSource.gameObject,clipLength);

    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {

        // Assign a random index
        int rand = Random.Range(0, audioClip.Length);


        // Spawn in GameObject
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);

        // Assing clip
        audioSource.clip = audioClip[rand];

        // Assing volume
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Get length of sound
        float clipLength = audioSource.clip.length;

        // Destroy clip after done playing
        Destroy(audioSource.gameObject, clipLength);

    }






}
