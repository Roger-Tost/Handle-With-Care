using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_CharacterMovement : MonoBehaviour
{
    // Componentes del personaje
    private CharacterController controller;
    private GameObject mainCamera;

    // Movimiento
    public GameObject characterModel; // Modelo del personaje para rotarlo
    public float moveSpeed = 5f;      // Velocidad de movimiento
    private Vector3 moveDirection;
    private Vector3 velocity;
    private float gravity = -9.8f;
    private float ySpeed;

    // Interacci�n
    public float interactionRange = 3f; // Rango de interacci�n
    private GameObject heldObject = null;
    public Transform hand;              // Punto donde se posicionar� el objeto recogido

    void Start()
    {
        // Inicializaci�n de componentes
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main.gameObject;
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
        // Obtener inputs de movimiento
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calcular direcci�n del movimiento
        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * moveDirection;
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

        // Rotar el modelo del personaje hacia la direcci�n del movimiento
        if (moveDirection.magnitude > 0)
        {
            characterModel.transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    // Interacci�n con objetos
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

        // Si hay un objeto recogido, posicionarlo en la mano
        if (heldObject != null)
        {
            heldObject.transform.position = hand.position;
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
                    rb.isKinematic = true; // Desactivar f�sicas
                    rb.useGravity = false;
                }
            }
            else if (hit.transform.CompareTag("Chair"))
            {
                SwitchToFixedCamera(); // Cambia la c�mara seg�n lo que requieras
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
                rb.isKinematic = false; // Reactivar f�sicas
                rb.useGravity = true;
                rb.AddForce(mainCamera.transform.forward * 2f, ForceMode.Impulse); // Peque�a fuerza
            }

            heldObject = null;
        }
    }

    // Cambiar a c�mara fija (si es necesario implementar algo relacionado con c�maras en el futuro)
    void SwitchToFixedCamera()
    {
        Debug.Log("Interacci�n con silla detectada. Aqu� podr�as activar una c�mara fija.");
        // L�gica para cambiar de c�mara si en un futuro decides implementar algo.
    }
}



