using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PICKUP : MonoBehaviour
{
    [Header("Referencias Inspector")]
    [Tooltip("Objeto a recoger (GameObject que tenga Collider y Rigidbody)")]
    public GameObject pickup;

    [Tooltip("Transform que representa la mano o punto de anclaje del jugador")]
    public Transform mano;

    [Header("UI Interacción")]
    [SerializeField] private GameObject interactionPromptUI;

    // Variables de estado
    public bool activo;
    public bool objetoEnMano;
    public bool Comprobante;

    // Componentes en runtime
    public Scr_PosicionObjetos PosicionObjetos;
    public Collider pickupCollider;
    public Rigidbody pickupRigidbody;

    // Escala y rotación original para restaurar
    public Vector3 escalaOriginal;
    public Quaternion rotacionOriginal;

    public static bool EstaSosteniendoObjeto = false;

    public void Start()
    {
        if (pickup == null) Debug.LogError("[Pickup] ERROR: 'pickup' no asignado en Inspector.");
        if (mano == null) Debug.LogError("[Pickup] ERROR: 'mano' no asignado en Inspector.");

        if (pickup != null)
        {
            escalaOriginal = pickup.transform.localScale;
            rotacionOriginal = pickup.transform.rotation;

            pickupCollider = pickup.GetComponent<Collider>();
            if (pickupCollider == null) Debug.LogError("[Pickup] ERROR: El objeto 'pickup' necesita un Collider.");

            pickupRigidbody = pickup.GetComponent<Rigidbody>();
            if (pickupRigidbody == null)
            {
                pickupRigidbody = pickup.AddComponent<Rigidbody>();
                Debug.LogWarning("[Pickup] Rigidbody agregado automáticamente al objeto pickup.");
            }

            pickupRigidbody.useGravity = true;
            pickupRigidbody.isKinematic = false;
        }

        PosicionObjetos = FindObjectOfType<Scr_PosicionObjetos>();

        if (interactionPromptUI != null)
            interactionPromptUI.SetActive(false);
    }

    public void Update()
    {
        if (activo && Input.GetKeyDown(KeyCode.E) && !objetoEnMano && !EstaSosteniendoObjeto)
        {
            PillarObjeto();
            EstaSosteniendoObjeto = true;

            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && objetoEnMano)
        {
            SoltarObjeto();
            EstaSosteniendoObjeto = false;
            Debug.Log("[Pickup] Tecla R presionada.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!objetoEnMano && other.CompareTag("Player"))
        {
            activo = true;
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(true);
        }

        if (other.CompareTag("Comprobante"))
            SetComprobante(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!objetoEnMano && other.CompareTag("Player"))
        {
            activo = false;
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(false);
        }

        if (other.CompareTag("Comprobante"))
            SetComprobante(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Comprobante"))
            SetComprobante(true);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Comprobante"))
            SetComprobante(false);
    }

    private void SetComprobante(bool value)
    {
        Comprobante = value;
        Debug.Log("[Pickup] Comprobante=" + Comprobante);
    }

    public void PillarObjeto()
    {
        if (pickup == null || mano == null) return;

        pickup.transform.SetParent(mano, true);
        pickup.transform.position = mano.position;
        pickup.transform.rotation = rotacionOriginal;

        if (pickupRigidbody != null)
            pickupRigidbody.isKinematic = true;
        if (pickupCollider != null)
            pickupCollider.isTrigger = true;

        objetoEnMano = true;
        Debug.Log("[Pickup] En mano. objetoEnMano=" + objetoEnMano);
    }

    public void SoltarObjeto()
    {
        DejarObjeto();
    }

    public void DejarObjeto()
    {
        if (pickup == null) return;

        pickup.transform.SetParent(null, true);
        pickup.transform.localScale = escalaOriginal;
        pickup.transform.rotation = rotacionOriginal;

        if (pickupRigidbody != null)
        {
            pickupRigidbody.isKinematic = false;
            pickupRigidbody.useGravity = true;
        }
        if (pickupCollider != null)
            pickupCollider.isTrigger = false;

        objetoEnMano = false;
        activo = false;
        Debug.Log("[Pickup] Soltado. objetoEnMano=" + objetoEnMano + ", activo=" + activo);

        if (pickupRigidbody != null)
            pickupRigidbody.AddForce(mano.forward * 0.5f, ForceMode.Impulse);
    }
}
