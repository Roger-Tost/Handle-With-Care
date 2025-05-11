using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Scr_Doors : MonoBehaviour
{
    [SerializeField] private AudioClip OpenDoorClip;

    [Header("Referencia al Animator que controla la animación de la puerta")]
    public Animator doorAnimator;

    [Header("Estado de la puerta")]
    public bool isOpen = false;
    public bool isLocked;

    [Header("UI")]
    [SerializeField] private GameObject interactText;

    // Indica si el jugador está dentro del área de interacción
    public bool playerInRange = false;

    private void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked)
            {
                isOpen = !isOpen;
                doorAnimator.SetBool("isOpen", isOpen);

                Scr_SoundManager.instance.PlaySoundFXClip(OpenDoorClip, transform, 1f);
            }
            else
            {
                doorAnimator.SetTrigger("isLocked");
            }

            // Hide prompt after interaction
            if (interactText != null)
                interactText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!isLocked && interactText != null)
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
