using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sit : MonoBehaviour
{
    public Camera mainCamera;       // Referencia a la cámara principal
    public Camera secondaryCamera; // Referencia a la segunda cámara
    private bool isPlayerInRange = false; // Bandera para saber si el jugador está en el rango
    private bool isMovementBlocked = false; // Bandera para controlar el bloqueo del movimiento del jugador

    public GameObject player; // Referencia al jugador
    public float mouseSensitivity = 100f; // Sensibilidad del ratón

    private float xRotation = 0f; // Control de la rotación vertical de la cámara

    private void Start()
    {
        // Asegúrate de que la cámara principal esté activa al inicio
        if (mainCamera != null) mainCamera.enabled = true;
        if (secondaryCamera != null) secondaryCamera.enabled = false;

        // Bloquear el cursor al inicio
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador entra al rango (puedes usar el tag "Player" para identificarlo)
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador sale del rango
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        // Si el jugador está en rango y presiona la tecla E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Cambia entre las cámaras
            if (mainCamera != null && secondaryCamera != null)
            {
                mainCamera.enabled = !mainCamera.enabled;
                secondaryCamera.enabled = !secondaryCamera.enabled;
            }

            // Bloquea o desbloquea el movimiento del jugador
            isMovementBlocked = !isMovementBlocked;

            if (player != null)
            {
                var playerMovement = player.GetComponent<scr_CharacterMovement>(); // Suponiendo que tu script de movimiento se llama "PlayerMovement"
                if (playerMovement != null)
                {
                    playerMovement.enabled = !isMovementBlocked;
                }
            }

            // Cambia el estado del cursor al alternar cámaras
            Cursor.lockState = secondaryCamera.enabled ? CursorLockMode.Locked : CursorLockMode.None;
        }

        // Si la cámara secundaria está activa, permite su rotación
        if (secondaryCamera != null && secondaryCamera.enabled)
        {
            RotateSecondaryCamera();
        }
    }

    private void RotateSecondaryCamera()
    {
        // Obtener entrada del ratón solo en el eje Y (arriba/abajo)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Controlar la rotación vertical (eje X)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical para evitar que la cámara dé vueltas completas

        // Aplicar la rotación solo en el eje X (arriba/abajo)
        secondaryCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
