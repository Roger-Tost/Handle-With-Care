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

    private bool isPlayerInRange = false;
    private bool isPaused = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            isPlayerInRange = false;
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