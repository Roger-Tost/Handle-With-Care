using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MoverMueble : MonoBehaviour
{
    public GameObject objectToMove; // Objeto que queremos mover
    public Transform player; // Transform del jugador
    public GameObject textoCanvas; // Texto del Canvas que muestra la interacción
    public float moveSpeed = 5f; // Velocidad del movimiento
    public float moveDuration = 1.0f; // Duración del movimiento

    private bool isPlayerNearby = false; // Para saber si el jugador está cerca
    private bool isMoving = false; // Para evitar múltiples movimientos simultáneos

    void Start()
    {
        // Asegurarse de que el texto esté desactivado al inicio
        if (textoCanvas != null)
        {
            textoCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Si el jugador está cerca y presiona "E", mueve el objeto
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(MoveObject());
            textoCanvas.SetActive(false); // Ocultar el texto cuando se inicia la acción
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el jugador entra al área
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (textoCanvas != null)
            {
                textoCanvas.SetActive(true); // Mostrar el texto en el Canvas
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectar si el jugador sale del área
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (textoCanvas != null)
            {
                textoCanvas.SetActive(false); // Ocultar el texto en el Canvas
            }
        }
    }

    private System.Collections.IEnumerator MoveObject()
    {
        isMoving = true;

        // Dirección hacia donde está mirando el jugador
        Vector3 moveDirection = player.forward;

        // Posición inicial y final del objeto
        Vector3 startPosition = objectToMove.transform.position;
        Vector3 targetPosition = startPosition + moveDirection * moveSpeed;

        float elapsedTime = 0f;

        // Interpolar la posición del objeto
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            objectToMove.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }

        // Asegurar la posición final
        objectToMove.transform.position = targetPosition;

        isMoving = false;
    }
}
