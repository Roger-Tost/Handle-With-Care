using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Scr_Doors : MonoBehaviour
{
    [SerializeField] private AudioClip OpenDoorClip;


    [Header("Referencia al Animator que controla la animaci�n de la puerta")]
    public Animator doorAnimator;

    [Header("Estado de la puerta")]
    public bool isOpen = false;
    public bool isLocked;

    // Indica si el jugador est� dentro del �rea de interacci�n
    public bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked)
            {
                isOpen = !isOpen;
                doorAnimator.SetBool("isOpen", isOpen);

                // Opening Door Sound
                Scr_SoundManager.instance.PlaySoundFXClip(OpenDoorClip, transform, 1f);

            }

            else if (isLocked)
            {
                doorAnimator.SetTrigger("isLocked");

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
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