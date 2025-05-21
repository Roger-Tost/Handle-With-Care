using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicoSofa : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tag que debe tener el jugador")]
    [SerializeField] private string playerTag = "Player";
    [Tooltip("Tecla para interactuar (abrir/cerrar)")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [Tooltip("Tecla para cambiar de página")]
    [SerializeField] private KeyCode nextPageKey = KeyCode.R;

    [Header("UI")]
    [Tooltip("GameObjects de las imágenes (como páginas)")]
    [SerializeField] private GameObject[] pauseImages;
    [Tooltip("UI Text que muestra el mensaje de interacción")]
    [SerializeField] private GameObject interactText;

    private bool isPlayerInRange = false;
    private bool isPaused = false;
    private int currentPageIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = true;
            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = false;
            if (interactText != null)
                interactText.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            if (!isPaused)
            {
                PauseAndShowFirstPage();
            }
            else
            {
                ResumeAndHideAll();
            }
        }

        if (isPaused && Input.GetKeyDown(nextPageKey))
        {
            ShowNextPage();
        }
    }

    private void PauseAndShowFirstPage()
    {
        Time.timeScale = 0f;
        isPaused = true;
        currentPageIndex = 0;
        ShowPage(currentPageIndex);
        if (interactText != null)
            interactText.SetActive(false);
    }

    private void ResumeAndHideAll()
    {
        Time.timeScale = 1f;
        isPaused = false;
        HideAllPages();
        currentPageIndex = 0;
    }

    private void ShowNextPage()
    {
        if (currentPageIndex + 1 < pauseImages.Length)
        {
            currentPageIndex++;
            ShowPage(currentPageIndex);
        }
        // Si llegaste a la última imagen, ya no hace nada al presionar R
    }

    private void ShowPage(int index)
    {
        for (int i = 0; i < pauseImages.Length; i++)
        {
            if (pauseImages[i] != null)
                pauseImages[i].SetActive(i == index);
        }
    }

    private void HideAllPages()
    {
        foreach (GameObject img in pauseImages)
        {
            if (img != null)
                img.SetActive(false);
        }
    }
}
