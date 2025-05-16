using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    [Header("Panel del inventario")]
    public GameObject inventoryPanel;

    [Header("Sistema de notas")]
    public bool nota1 = false;                  // Esta la puedes activar desde otro script
    public GameObject nota1Image;               // Imagen que se mostrará si nota1 es true

    private bool isInventoryOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (nota1Image != null)
            nota1Image.SetActive(false); // Asegúrate de que la imagen esté desactivada al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Activar la imagen de la nota si la booleana es true
        if (nota1 && nota1Image != null && !nota1Image.activeSelf)
        {
            nota1Image.SetActive(true);
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        inventoryPanel.SetActive(isInventoryOpen);
        Time.timeScale = isInventoryOpen ? 0f : 1f;

        Cursor.visible = isInventoryOpen;
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
