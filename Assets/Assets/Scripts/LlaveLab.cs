using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LlaveLab : MonoBehaviour
{
    public bool GetKey = false;                    // Variable que se activa al presionar E
    public TextMeshProUGUI interactionText;        // Referencia al texto TMP en la UI

    private bool isPlayerInRange = false;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Ocultar texto al inicio
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GetKey = true;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false); // Ocultar el texto
            }
            gameObject.SetActive(false); // Desactiva este GameObject
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true); // Mostrar texto TMP
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false); // Ocultar texto TMP
            }
        }
    }
}
