using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MirrorActivation : MonoBehaviour
{
    [SerializeField] private Camera mirrorCamera; // Arrastra aquí la cámara del espejo desde el inspector

    private void Start()
    {
        if (mirrorCamera != null)
        {
            mirrorCamera.gameObject.SetActive(false); // Desactiva la cámara inicialmente
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mirrorCamera != null) // Verifica que el objeto que entra sea el jugador
        {
            mirrorCamera.gameObject.SetActive(true); // Activa la cámara del espejo
            Debug.Log("Espejo activado");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && mirrorCamera != null) // Verifica que el objeto que sale sea el jugador
        {
            mirrorCamera.gameObject.SetActive(false); // Desactiva la cámara del espejo
            Debug.Log("Espejo desactivado");
        }
    }
}
