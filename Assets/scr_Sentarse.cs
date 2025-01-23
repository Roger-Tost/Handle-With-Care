using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sentarse : MonoBehaviour
{
    public GameObject playerStanding, playerSitting, intText, standText;
    public Camera mainCamera; // Referencia a la c�mara principal
    public float cameraMoveSpeed = 2f; // Velocidad de movimiento de la c�mara
    public Vector3 cameraOffset; // Offset de la c�mara respecto al jugador
    public bool interactable, sitting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true); // Activa el texto de interacci�n
            interactable = true; // Permite la interacci�n
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false); // Desactiva el texto de interacci�n
            interactable = false; // Desactiva la interacci�n
        }
    }

    void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!sitting) // Si no est� sentado
                {
                    intText.SetActive(false);
                    standText.SetActive(true);
                    playerSitting.SetActive(true);
                    sitting = true;
                    playerStanding.SetActive(false);
                    StartCoroutine(MoveCameraToPlayer(playerSitting.transform)); // Mueve la c�mara al jugador sentado
                }
                else // Si ya est� sentado
                {
                    playerSitting.SetActive(false);
                    standText.SetActive(false);
                    playerStanding.SetActive(true);
                    sitting = false;
                    StartCoroutine(MoveCameraToPlayer(playerStanding.transform)); // Mueve la c�mara al jugador de pie
                }
            }
        }
    }

    private IEnumerator MoveCameraToPlayer(Transform target)
    {
        Vector3 targetPosition = target.position + cameraOffset; // Calcula la posici�n objetivo de la c�mara
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f) // Mientras no est� cerca de la posici�n objetivo
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime); // Mueve la c�mara suavemente
            mainCamera.transform.LookAt(target); // Hace que la c�mara mire hacia el objetivo
            yield return null; // Espera un frame
        }
        mainCamera.transform.position = targetPosition; // Aseg�rate de que la c�mara est� exactamente en la posici�n objetivo
    }
}
