using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Scr_Doors : MonoBehaviour
{
    [Header("Referencia al Animator que controla la animación de la puerta")]
    public Animator doorAnimator;

    [Header("Estado de la puerta")]
    public bool isOpen = false;

    // Indica si el jugador está dentro del área de interacción
    public bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            doorAnimator.SetBool("isOpen", isOpen);
            Debug.Log($"Puerta ahora está {(isOpen ? "ABIERTA" : "CERRADA")}");
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