using UnityEngine;
using UnityEngine.UI;

public class SCR_PeriodicoInteract : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tag que debe tener el jugador")]
    [SerializeField] private string playerTag = "Player";
    [Tooltip("Tecla para interactuar")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    [Tooltip("GameObject de la imagen en el Canvas (debe estar desactivado al inicio)")]
    [SerializeField] private GameObject pauseImage;
    [Tooltip("UI Text que muestra el mensaje de interacción (debe estar desactivado al inicio)")]
    [SerializeField] private GameObject interactText;  // UI element showing [E] to interact

    private bool isPlayerInRange = false;
    private bool isPaused = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = true;
            // Show interaction prompt when player enters the range
            if (interactText != null)
                interactText.SetActive(true);  // Show the UI text
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = false;
            // Hide interaction prompt when player leaves the range
            if (interactText != null)
                interactText.SetActive(false);  // Hide the UI text
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            TogglePause();
            // Hide interaction prompt once player interacts with the object
            if (interactText != null)
                interactText.SetActive(false);  // Hide the UI text once interacted
        }
    }

    private void TogglePause()
    {
        if (!isPaused)
        {
            // Pausar el juego
            Time.timeScale = 0f;
            // Mostrar la imagen
            if (pauseImage != null)
                pauseImage.SetActive(true);
            isPaused = true;
        }
        else
        {
            // Reanudar el juego
            Time.timeScale = 1f;
            // Ocultar la imagen
            if (pauseImage != null)
                pauseImage.SetActive(false);
            isPaused = false;
        }
    }
}
