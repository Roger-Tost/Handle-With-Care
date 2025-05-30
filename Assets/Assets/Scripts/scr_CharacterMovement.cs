using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_CharacterMovement : MonoBehaviour
{
    [SerializeField] private AudioClip[] FootstepsSoundClips;
    [SerializeField] private float walkStepInterval = 0.5f;
    private float footstepTimer = 0f;

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

    // Interacci�n
    public float interactionRange = 3f; // Rango de interacci�n
    private GameObject heldObject = null;
    public Transform hand;              // Punto donde se posicionar� el objeto recogido

    void Start()
    {
        CanMove = true;
        // Inicializaci�n de componentes
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

            // Calcular direcci�n del movimiento
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

            // Reproducir sonido de pasos con delay
            if (moveDirection.magnitude > 0.1f)
            {
                footstepTimer -= Time.deltaTime;
                if (footstepTimer <= 0f && controller.isGrounded)
                {
                    Scr_SoundManager.instance.PlayRandomSoundFXClip(FootstepsSoundClips, transform, 0.8f);
                    footstepTimer = walkStepInterval;
                }
            }
            else
            {
                footstepTimer = 0f;
            }

            // Rotar el modelo del personaje hacia la direcci�n del movimiento
            if (moveDirection.magnitude > 0)
            {
                characterModel.transform.rotation = Quaternion.LookRotation(moveDirection);
            }
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

        // Si hay un objeto recogido, posicionarlo en la mano y hacer que siga la c�mara
        if (heldObject != null)
        {
            heldObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 0.5f;
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
                heldObject = hit.transform.gameObject;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
            }
            else if (hit.transform.CompareTag("Chair"))
            {
                SwitchToFixedCamera();
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
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(mainCamera.transform.forward * 2f, ForceMode.Impulse);
            }

            heldObject = null;
        }
    }

    // Cambiar a c�mara fija (si es necesario implementar algo relacionado con c�maras en el futuro)
    void SwitchToFixedCamera()
    {
        Debug.Log("Interacci�n con silla detectada. Aqu� podr�as activar una c�mara fija.");
    }
}