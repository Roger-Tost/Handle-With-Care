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

    [Tooltip("Texto para mostrar cuando el jugador puede interactuar")]
    [SerializeField] private GameObject interactionText;

    private bool isPlayerInRange = false;
    private bool isPaused = false;

    private void Start()
    {
        // Ensure interaction text is hidden at the start
        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = true;
            // Show interaction text when player is within range
            if (interactionText != null)
            {
                interactionText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = false;
            // Hide interaction text when player exits the range
            if (interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (!isPaused)
        {
            // Pausar el juego
            Time.timeScale = 0f;
            // Mostrar la imagen
            pauseImage.SetActive(true);
            isPaused = true;
        }
        else
        {
            // Reanudar el juego
            Time.timeScale = 1f;
            // Ocultar la imagen
            pauseImage.SetActive(false);
            isPaused = false;
        }
    }
}
