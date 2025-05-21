using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LlaveLab : MonoBehaviour
{
    public bool GetKey = false;
    public TextMeshProUGUI interactionText;

    private bool isPlayerInRange = false;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GetKey = true;

            if (interactionText != null)
            {
                interactionText.text = "¡Llave recogida!";
                StartCoroutine(HideInteractionTextAfterSeconds(2f)); // Oculta el texto tras 2 segundos
            }

            // Desactivamos solo la parte visual y el collider de la llave
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionText != null && !GetKey)
            {
                interactionText.text = "Pulsa E para recoger la llave";
                interactionText.gameObject.SetActive(true);
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
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator HideInteractionTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
