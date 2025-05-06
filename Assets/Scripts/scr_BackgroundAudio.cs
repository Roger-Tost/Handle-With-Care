using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BackgroundAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log("[Audio] Reproduciendo audio al iniciar escena.");
        }
        else
        {
            Debug.LogWarning("[Audio] No se encontró AudioSource o clip asignado.");
        }
    }
}
