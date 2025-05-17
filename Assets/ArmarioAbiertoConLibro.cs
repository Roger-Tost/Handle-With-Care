using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmarioAbiertoConLibro : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    [SerializeField] private GameObject pauseImage;
    [SerializeField] private GameObject interactText;

    [Header("Inventario")]
    [Tooltip("Referencia al script del inventario (InventoryUI)")]
    [SerializeField] private InvetoryUI inventoryUI;

    [Tooltip("Número de nota a activar (1-4)")]
    [Range(1, 4)]
    [SerializeField] private int notaNumero = 1;

    private bool isPlayerInRange = false;
    private bool isPaused = false;

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
            ActivarNota(); // <- Aquí se activa la nota
            TogglePause();
            if (interactText != null)
                interactText.SetActive(false);
        }
    }

    private void ActivarNota()
    {
        if (inventoryUI == null) return;

        switch (notaNumero)
        {
            case 1:
                inventoryUI.nota1 = true;
                break;
            case 2:
                inventoryUI.nota2 = true;
                break;
            case 3:
                inventoryUI.nota3 = true;
                break;
            case 4:
                inventoryUI.nota4 = true;
                break;
            default:
                Debug.LogWarning("Número de nota fuera de rango (debe ser 1 a 4)");
                break;
        }
    }

    private void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            if (pauseImage != null)
                pauseImage.SetActive(true);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            if (pauseImage != null)
                pauseImage.SetActive(false);
            isPaused = false;
        }
    }
}
