using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class scr_VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Componente VideoPlayer asociado al objeto
    private bool activo; // Determina si el trigger está activo

    void Update()
    {
        if (activo && Input.GetKeyDown(KeyCode.E)) // Presionar 'E' dentro del trigger
        {
            if (videoPlayer != null)
            {
                MostrarVideo();
            }
        }

        if (activo && Input.GetKeyDown(KeyCode.R)) // Presionar 'E' dentro del trigger
        {
            if (videoPlayer != null)
            {
                ApagarVideo();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que entra tiene el tag "Player"
        {
            activo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que sale tiene el tag "Player"
        {
            activo = false;
        }
    }

    public void MostrarVideo()
    {
        videoPlayer.Play(); // Reproducir el video
    }

    public void ApagarVideo()
    {
        videoPlayer.Stop();
    }
}
