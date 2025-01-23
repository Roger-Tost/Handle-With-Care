using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sentarse : MonoBehaviour
{
    public GameObject playerStanding, playerSitting, intText, standText;
    public Camera mainCamera; // Referencia a la cámara principal
    public float cameraMoveSpeed = 2f; // Velocidad de movimiento de la cámara
    public Vector3 cameraOffset; // Offset de la cámara respecto al jugador
    public bool interactable, sitting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true); // Activa el texto de interacción
            interactable = true; // Permite la interacción
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false); // Desactiva el texto de interacción
            interactable = false; // Desactiva la interacción
        }
    }

    void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!sitting) // Si no está sentado
                {
                    intText.SetActive(false);
                    standText.SetActive(true);
                    playerSitting.SetActive(true);
                    sitting = true;
                    playerStanding.SetActive(false);
                    StartCoroutine(MoveCameraToPlayer(playerSitting.transform)); // Mueve la cámara al jugador sentado
                }
                else // Si ya está sentado
                {
                    playerSitting.SetActive(false);
                    standText.SetActive(false);
                    playerStanding.SetActive(true);
                    sitting = false;
                    StartCoroutine(MoveCameraToPlayer(playerStanding.transform)); // Mueve la cámara al jugador de pie
                }
            }
        }
    }

    private IEnumerator MoveCameraToPlayer(Transform target)
    {
        Vector3 targetPosition = target.position + cameraOffset; // Calcula la posición objetivo de la cámara
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f) // Mientras no esté cerca de la posición objetivo
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime); // Mueve la cámara suavemente
            mainCamera.transform.LookAt(target); // Hace que la cámara mire hacia el objetivo
            yield return null; // Espera un frame
        }
        mainCamera.transform.position = targetPosition; // Asegúrate de que la cámara esté exactamente en la posición objetivo
    }
}
