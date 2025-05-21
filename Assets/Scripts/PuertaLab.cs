using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuertaLab : MonoBehaviour
{
    [SerializeField] private AudioClip OpenDoorClip;

    [Header("Animación")]
    public Animator doorAnimator;

    [Header("Estado de la puerta")]
    public bool isOpen = false;
    public bool isLocked;              // Puerta bloqueada estándar
    public bool isLockedWithKeyLab;    // Puerta bloqueada por llave

    [Header("UI")]
    [SerializeField] private GameObject interactText;
    [SerializeField] private TextMeshProUGUI missingKeyText;  // Texto TMP: "Te falta una llave"

    [Header("Referencia a la llave")]
    public LlaveLab keyPickup;  // Asignar el script de la llave desde el Inspector

    public bool playerInRange = false;

    private void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);

        if (missingKeyText != null)
            missingKeyText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Si la puerta está bloqueada por llave
            if (isLockedWithKeyLab)
            {
                if (keyPickup != null && keyPickup.GetKey)
                {
                    AbrirPuerta();
                }
                else
                {
                    if (missingKeyText != null)
                        missingKeyText.gameObject.SetActive(true);
                }
            }
            else if (!isLocked)
            {
                AbrirPuerta();
            }
            else
            {
                doorAnimator.SetTrigger("isLocked");
            }

            // Ocultar texto de interacción después de intentar abrir
            if (interactText != null)
                interactText.SetActive(false);
        }
    }

    private void AbrirPuerta()
    {
        isOpen = !isOpen;
        doorAnimator.SetBool("isOpen", isOpen);
        Scr_SoundManager.instance.PlaySoundFXClip(OpenDoorClip, transform, 1f);

        if (missingKeyText != null)
            missingKeyText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!isLocked && !isLockedWithKeyLab && interactText != null)
                interactText.SetActive(true);
            else if ((isLockedWithKeyLab || isLocked) && interactText != null)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactText != null)
                interactText.SetActive(false);

            if (missingKeyText != null)
                missingKeyText.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Collider col = GetComponent<Collider>();
        if (col != null && col.isTrigger)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            if (col is BoxCollider box)
                Gizmos.DrawWireCube(box.center, box.size);
            else if (col is SphereCollider sphere)
                Gizmos.DrawWireSphere(sphere.center, sphere.radius);
        }
    }
}
