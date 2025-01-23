using UnityEngine;

public class TeleportToChair : MonoBehaviour
{
    public Transform chair; // Asigna la silla en el inspector
    private Vector3 originalPosition; // Para almacenar la posici�n original
    private bool isSitting = false; // Para saber si el personaje est� sentado

    void Update()
    {
        // Detectar la pulsaci�n de la tecla "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isSitting)
            {
                TeleportToChairPosition();
            }
            else
            {
                ReturnToOriginalPosition();
            }
        }
    }

    void TeleportToChairPosition()
    {
        // Guardar la posici�n original
        originalPosition = transform.position;

        // Teletransportar al personaje a la silla
        transform.position = chair.position;

        // Bloquear el movimiento (puedes desactivar el componente de movimiento aqu�)
        isSitting = true;
        // Si tienes un componente de movimiento, desact�valo aqu�
        // GetComponent<CharacterController>().enabled = false; // Descomenta si usas CharacterController
    }

    void ReturnToOriginalPosition()
    {
        // Regresar a la posici�n original
        transform.position = originalPosition;

        // Permitir el movimiento nuevamente
        isSitting = false;
        // Si desactivaste el componente de movimiento, act�valo aqu�
        // GetComponent<CharacterController>().enabled = true; // Descomenta si usas CharacterController
    }
}