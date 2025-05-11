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

    [Header("Interaction Text UI")]
    [Tooltip("UI Text for interaction prompt")]
    public GameObject interactionText; // Reference to the [E] to interact text

    private void Start()
    {
        // Ensure the Canvas is inactive when the game starts
        if (pcCanvas != null)
            pcCanvas.SetActive(false);

        // Ensure interaction text is hidden at the start
        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
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

                // Hide the interaction text when the PC interface is activated
                if (interactionText != null)
                {
                    interactionText.SetActive(false);
                }
            }
            else
            {
                // If exiting, lock cursor again and hide it
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // Optionally, re-enable the interaction text when the player exits the PC interface
                if (interactionText != null)
                {
                    interactionText.SetActive(true);
                }
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

            // Show interaction text when player is within range of the PC
            if (interactionText != null)
            {
                interactionText.SetActive(true);
            }
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

            // Hide interaction text when player leaves the PC area
            if (interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }
    }
}
