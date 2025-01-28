using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class scr_VideoTrigger : MonoBehaviour
{
    public GameObject textoCanvas; // Referencia al texto en el Canvas
    public VideoPlayer videoPlayer; // Componente VideoPlayer asociado al objeto
    private bool activo; // Determina si el trigger está activo

    void Update()
    {
        if (activo && Input.GetKeyDown(KeyCode.E)) // Presionar 'E' dentro del trigger
        {
            if (videoPlayer != null)
            {
                videoPlayer.Play(); // Reproducir el video
                textoCanvas.SetActive(false); // Ocultar el texto al reproducir el video
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que entra tiene el tag "Player"
        {
            activo = true;
            textoCanvas.SetActive(true); // Mostrar texto indicando que se puede interactuar
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que sale tiene el tag "Player"
        {
            activo = false;
            textoCanvas.SetActive(false); // Ocultar el texto al salir del trigger
        }
    }
}
