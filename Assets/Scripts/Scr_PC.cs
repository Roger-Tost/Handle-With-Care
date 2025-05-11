using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_PC : MonoBehaviour
{
    public Scr_SistemaOrdenador PCinterfaz; // Assuming this handles the PC UI logic
    public bool IsOnPC = false;
    [SerializeField] private AudioClip NotificationSoundClip;
    public GameObject pcCanvas; // Canvas that will be shown/hidden
    private bool isCanvasActive = false; // Track canvas state

    private void Start()
    {
        // Ensure the Canvas is inactive when the game starts
        if (pcCanvas != null)
            pcCanvas.SetActive(false);
    }

    private void Update()
    {
        // If player is nearby and presses 'E', activate the PC interface
        if (IsOnPC && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle canvas visibility
            if (pcCanvas != null)
            {
                isCanvasActive = !isCanvasActive;
                pcCanvas.SetActive(isCanvasActive); // Show/Hide Canvas
            }

            // If the PC interface is activated, trigger the relevant action
            if (isCanvasActive)
            {
                PCinterfaz.ActivarPC(); // Assuming this method handles the PC functionality
                Cursor.lockState = CursorLockMode.None;  // Unlock cursor for UI interaction
                Cursor.visible = true;                   // Show cursor
            }
            else
            {
                // If exiting, lock cursor again and hide it
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsOnPC = true;
            // Play notification sound when player enters
            Scr_SoundManager.instance.PlaySoundFXClip(NotificationSoundClip, transform, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsOnPC = false;
            if (isCanvasActive)
            {
                // Deactivate Canvas when the player leaves, if active
                if (pcCanvas != null)
                    pcCanvas.SetActive(false);

                // Lock and hide the cursor again
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
