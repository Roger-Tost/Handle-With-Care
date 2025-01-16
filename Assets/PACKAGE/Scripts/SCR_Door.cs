using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Door : MonoBehaviour
{
    // Variable para saber si el jugador tiene la llave
    public bool HasKey = false;

    // Puerta de la que se va a modificar el estado (abrir/cerrar)
    public Transform puerta;
    public float openAngle = 90f;  // �ngulo para abrir la puerta
    public float speed = 3f;       // Velocidad de apertura/cierre

    // Verifica si el jugador est� dentro del �rea de la puerta
    private bool playerInRange = false;

    private bool isOpening = false;
    private bool isClosing = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Establecer la rotaci�n inicial (cerrada)
        closedRotation = puerta.rotation;
        openRotation = Quaternion.Euler(puerta.rotation.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        // Si el jugador est� dentro del �rea de la puerta y tiene la llave, puede abrirla
        if (playerInRange && HasKey && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpening && !isClosing)
            {
                isOpening = true;
            }
        }

        // Si la puerta est� abriendo, interpolamos su rotaci�n hacia la posici�n abierta
        if (isOpening)
        {
            puerta.rotation = Quaternion.Slerp(puerta.rotation, openRotation, speed * Time.deltaTime);
            if (Quaternion.Angle(puerta.rotation, openRotation) < 1f)
            {
                isOpening = false;
            }
        }
    }

    // Detecta cuando el jugador entra en el �rea de la puerta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;  // El jugador ha entrado en el �rea de la puerta
        }
    }

    // Detecta cuando el jugador sale del �rea de la puerta
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // El jugador ha salido del �rea de la puerta
        }
    }
}
