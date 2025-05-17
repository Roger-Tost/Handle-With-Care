using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    [Header("Panel del inventario")]
    public GameObject inventoryPanel;

    [Header("Sistema de notas")]
    public bool nota1 = false;
    public GameObject nota1Image;

    public bool nota2 = false;
    public GameObject nota2Image;

    public bool nota3 = false;
    public GameObject nota3Image;

    public bool nota4 = false;
    public GameObject nota4Image;

    private bool isInventoryOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (nota1Image != null) nota1Image.SetActive(false);
        if (nota2Image != null) nota2Image.SetActive(false);
        if (nota3Image != null) nota3Image.SetActive(false);
        if (nota4Image != null) nota4Image.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Mostrar notas si están activadas y aún no visibles
        if (nota1 && nota1Image != null && !nota1Image.activeSelf)
            nota1Image.SetActive(true);

        if (nota2 && nota2Image != null && !nota2Image.activeSelf)
            nota2Image.SetActive(true);

        if (nota3 && nota3Image != null && !nota3Image.activeSelf)
            nota3Image.SetActive(true);

        if (nota4 && nota4Image != null && !nota4Image.activeSelf)
            nota4Image.SetActive(true);
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
