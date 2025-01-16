using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    GameObject Cam;
    public GameObject Character;
    public float MoveSpeed;
    public Vector3 MoveDirection;
    public Vector3 MoveVelocity;
    public float distanciaDeRecogida = 3f;
    private GameObject objetoRecogido = null;
    public Transform mano;



    float ySpeed;
    float Gravity = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cam = Camera.main.gameObject; // Obtiene la c�mara principal
    }

    // Update is called once per frame
    void Update()
    {
        // Recibe Inputs
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");

        // Direccionar el Vector de Movimiento
        MoveDirection = new Vector3(HorizontalInput, 0, VerticalInput);
        MoveDirection = Quaternion.AngleAxis(Cam.transform.rotation.eulerAngles.y, Vector3.up) * MoveDirection;
        MoveDirection.Normalize();

        // Aplica gravedad
        SetGravity();

        // C�lculo de velocidades
        MoveVelocity = MoveDirection * MoveSpeed;
        MoveVelocity.y += ySpeed;

        // Movimiento final
        controller.Move(MoveVelocity * Time.deltaTime);

        // **Rotar el personaje en el eje Y hacia la direcci�n de la c�mara**
        RotateCharacter();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objetoRecogido == null)
            {
                PickupIntent();
            }
            else
            {
                SoltarObjeto();
            }
        }

        if (objetoRecogido != null)
        {
            objetoRecogido.transform.position = mano.position;
        }



    }

    void RotateCharacter()
    {
        // Obtiene la rotaci�n en Y de la c�mara
        float targetYRotation = Cam.transform.rotation.eulerAngles.y;

        // Obtiene la rotaci�n actual del personaje
        Vector3 currentRotation = Character.transform.rotation.eulerAngles;

        // Mant�n los valores actuales de X y Z, y modifica solo el Y
        Character.transform.rotation = Quaternion.Euler(currentRotation.x, targetYRotation, currentRotation.z);
    }

    void SetGravity()
    {
        if (controller.isGrounded)
        {
            ySpeed = -1f;
        }
        else
        {
            ySpeed += Gravity * Time.deltaTime;
        }
    }

    void PickupIntent()
    {
        RaycastHit hit;

        // Genera un rayo desde el centro de la c�mara hacia adelante
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, distanciaDeRecogida))
        {
            if (hit.transform.CompareTag("Pickup"))
            {
                objetoRecogido = hit.transform.gameObject;

                // Aseg�rate de que el Rigidbody est� configurado correctamente
                Rigidbody rb = objetoRecogido.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                // Opcional: Desactiva la gravedad si es necesario
                if (rb != null)
                {
                    rb.useGravity = false;
                }
            }
        }
    }


    void SoltarObjeto()
    {
        if (objetoRecogido != null)
        {
            Rigidbody rb = objetoRecogido.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false;

                // Activa la gravedad nuevamente
                rb.useGravity = true;

                // Opcional: A�ade una peque�a fuerza para evitar que el objeto quede est�tico
                rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
            }

            objetoRecogido = null;
        }
    }

}



