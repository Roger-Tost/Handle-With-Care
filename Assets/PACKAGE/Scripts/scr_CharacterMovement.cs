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
        Cam = Camera.main.gameObject; // Obtiene la cámara principal
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

        // Cálculo de velocidades
        MoveVelocity = MoveDirection * MoveSpeed;
        MoveVelocity.y += ySpeed;

        // Movimiento final
        controller.Move(MoveVelocity * Time.deltaTime);

        // **Rotar el personaje en el eje Y hacia la dirección de la cámara**
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
        // Obtiene la rotación en Y de la cámara
        float targetYRotation = Cam.transform.rotation.eulerAngles.y;

        // Obtiene la rotación actual del personaje
        Vector3 currentRotation = Character.transform.rotation.eulerAngles;

        // Mantén los valores actuales de X y Z, y modifica solo el Y
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

        // Genera un rayo desde el centro de la cámara hacia adelante
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, distanciaDeRecogida))
        {
            if (hit.transform.CompareTag("Pickup"))
            {
                objetoRecogido = hit.transform.gameObject;

                // Asegúrate de que el Rigidbody está configurado correctamente
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

                // Opcional: Añade una pequeña fuerza para evitar que el objeto quede estático
                rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
            }

            objetoRecogido = null;
        }
    }

}



