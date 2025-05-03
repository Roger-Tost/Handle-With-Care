using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_CharacterMovement : MonoBehaviour
{
    // Componentes del personaje
    private CharacterController controller;
    public GameObject mainCamera;

    // Movimiento
    public GameObject characterModel; // Modelo del personaje para rotarlo
    public float moveSpeed = 5f;      // Velocidad de movimiento
    private Vector3 moveDirection;
    private Vector3 velocity;
    private float gravity = -9.8f;
    private float ySpeed;
    public bool CanMove;

    // Interacción
    public float interactionRange = 3f; // Rango de interacción
    private GameObject heldObject = null;
    public Transform hand;              // Punto donde se posicionará el objeto recogido


    void Start()
    {
        CanMove = true;
        // Inicialización de componentes
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimiento del jugador
        HandleMovement();

        // Interacciones con objetos
        HandleInteraction();
    }

    // Movimiento del jugador
    void HandleMovement()
    {
        if (CanMove == true)
        {
            // Obtener inputs de movimiento
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Calcular dirección del movimiento
            moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            moveDirection = Quaternion.AngleAxis(mainCamera.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            moveDirection.Normalize();

            // Aplicar gravedad
            if (controller.isGrounded)
            {
                ySpeed = -1f; // Asegurar que no flote
            }
            else
            {
                ySpeed += gravity * Time.deltaTime;
            }

            // Calcular velocidad final
            velocity = moveDirection * moveSpeed;
            velocity.y = ySpeed;

            // Mover al personaje
            controller.Move(velocity * Time.deltaTime);

            // Rotar el modelo del personaje hacia la dirección del movimiento
            if (moveDirection.magnitude > 0)
            {
                characterModel.transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }
    }

    // Interacción con objetos
    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Tecla para interactuar
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }

        // Si hay un objeto recogido, posicionarlo en la mano y hacer que siga la cámara
        if (heldObject != null)
        {
            // Posiciona el objeto en la mano
            heldObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 0.5f;

            // Hace que el objeto siga la rotación de la cámara directamente
            heldObject.transform.rotation = mainCamera.transform.rotation;
        }
    }

    // Intentar recoger un objeto
    void TryPickupObject()
    {
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.transform.CompareTag("Pickup"))
            {
                // Recoger el objeto
                heldObject = hit.transform.gameObject;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true; // Desactivar físicas
                    rb.useGravity = false;
                }
            }
            else if (hit.transform.CompareTag("Chair"))
            {
                SwitchToFixedCamera(); // Cambia la cámara según lo que requieras
            }
        }
    }

    // Soltar el objeto recogido
    void DropObject()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false; // Reactivar físicas
                rb.useGravity = true;
                rb.AddForce(mainCamera.transform.forward * 2f, ForceMode.Impulse); // Pequeña fuerza
            }

            heldObject = null;
        }
    }

    // Cambiar a cámara fija (si es necesario implementar algo relacionado con cámaras en el futuro)
    void SwitchToFixedCamera()
    {
        Debug.Log("Interacción con silla detectada. Aquí podrías activar una cámara fija.");
        // Lógica para cambiar de cámara si en un futuro decides implementar algo.
    }
}
