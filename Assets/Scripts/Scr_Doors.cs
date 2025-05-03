using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Doors : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;

    private bool isOpen = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            doorAnimator.SetBool("isOpen", isOpen);
            Debug.Log("Puerta " + (isOpen ? "abierta" : "cerrada"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Jugador entró al área de la puerta");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Jugador salió del área de la puerta");
        }
    }
}
