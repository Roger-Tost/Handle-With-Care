using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MoveFurniture : MonoBehaviour
{
    public GameObject objectToMove; // Objeto que queremos mover
    public Transform player; // Transform del jugador
    public float moveSpeed = 5f; // Velocidad del movimiento
    public float moveDuration = 1.0f; // Duraci�n del movimiento

    private bool isPlayerNearby = false; // Para saber si el jugador est� cerca
    private bool isMoving = false; // Para evitar m�ltiples movimientos simult�neos

    void Start()
    {

    }

    void Update()
    {
        // Si el jugador est� cerca y presiona "E", mueve el objeto
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(MoveObject());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el jugador entra al �rea
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectar si el jugador sale del �rea
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private System.Collections.IEnumerator MoveObject()
    {
        isMoving = true;

        // Direcci�n hacia donde est� mirando el jugador
        Vector3 moveDirection = player.forward;

        // Posici�n inicial y final del objeto
        Vector3 startPosition = objectToMove.transform.position;
        Vector3 targetPosition = startPosition + moveDirection * moveSpeed;

        float elapsedTime = 0f;

        // Interpolar la posici�n del objeto
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            objectToMove.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }

        // Asegurar la posici�n final
        objectToMove.transform.position = targetPosition;

        isMoving = false;
    }
}
