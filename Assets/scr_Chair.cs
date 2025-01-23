using UnityEngine;

public class TeleportToChair : MonoBehaviour
{
    public Transform chair; // Asigna la silla en el inspector
    private Vector3 originalPosition; // Para almacenar la posición original
    private bool isSitting = false; // Para saber si el personaje está sentado

    void Update()
    {
        // Detectar la pulsación de la tecla "E"
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
        // Guardar la posición original
        originalPosition = transform.position;

        // Teletransportar al personaje a la silla
        transform.position = chair.position;

        // Bloquear el movimiento (puedes desactivar el componente de movimiento aquí)
        isSitting = true;
        // Si tienes un componente de movimiento, desactívalo aquí
        // GetComponent<CharacterController>().enabled = false; // Descomenta si usas CharacterController
    }

    void ReturnToOriginalPosition()
    {
        // Regresar a la posición original
        transform.position = originalPosition;

        // Permitir el movimiento nuevamente
        isSitting = false;
        // Si desactivaste el componente de movimiento, actívalo aquí
        // GetComponent<CharacterController>().enabled = true; // Descomenta si usas CharacterController
    }
}