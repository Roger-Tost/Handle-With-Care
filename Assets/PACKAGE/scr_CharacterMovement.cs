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
}
