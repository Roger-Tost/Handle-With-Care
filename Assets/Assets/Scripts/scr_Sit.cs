using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sit : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Transform sitCameraPosition; // Posición en la silla donde debe ubicarse la cámara

    private bool isPlayerInRange = false;
    private bool isMovementBlocked = false;

    public GameObject player;
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        if (mainCamera != null) mainCamera.enabled = true;
        if (secondaryCamera != null) secondaryCamera.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleSitting();
        }

        if (secondaryCamera.enabled)
        {
            RotateSecondaryCamera();
        }
    }

    private void ToggleSitting()
    {
        if (mainCamera != null && secondaryCamera != null)
        {
            if (!secondaryCamera.enabled)
            {
                // Posicionar la cámara en la silla antes de activarla
                secondaryCamera.transform.position = sitCameraPosition.position;
                secondaryCamera.transform.rotation = sitCameraPosition.rotation;
            }

            mainCamera.gameObject.SetActive(!mainCamera.enabled);
            secondaryCamera.gameObject.SetActive(!secondaryCamera.enabled);
        }

        isMovementBlocked = !isMovementBlocked;

        if (player != null)
        {
            var playerMovement = player.GetComponent<scr_CharacterMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = !isMovementBlocked;
            }

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = isMovementBlocked ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
            }
        }

        Cursor.lockState = secondaryCamera.enabled ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !secondaryCamera.enabled;
    }

    private void RotateSecondaryCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;

        secondaryCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}

