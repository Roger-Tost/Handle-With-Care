using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PickupObject : MonoBehaviour
{
    [Header("Referencias Inspector")]
    [Tooltip("Objeto a recoger (GameObject que tenga Collider y Rigidbody)")]
    public GameObject pickup;

    [Tooltip("Transform que representa la mano o punto de anclaje del jugador")]
    public Transform mano;

    [Header("Animaci�n")]
    [Tooltip("Animator que controla las animaciones del objeto pickup")]
    public Animator animator;

    [Header("Configuraci�n de identificaci�n")]
    [Tooltip("1 = teclado (l�gica especial de entrega)")]
    public int IDObjeto;

    // Variables de estado
    public bool activo;
    public bool objetoEnMano;
    public bool TieneElTeclado;
    public bool Comprobante; // Ser� true al tocar un objeto con tag "Comprobante"

    // Componentes en runtime
    public Scr_PosicionObjetos PosicionObjetos;
    public Collider pickupCollider;
    public Rigidbody pickupRigidbody;

    // Escala y rotaci�n original para restaurar
    public Vector3 escalaOriginal;
    public Quaternion rotacionOriginal;

    public void Start()
    {
        // Verificar referencias en Inspector
        if (pickup == null) Debug.LogError("[Pickup] ERROR: 'pickup' no asignado en Inspector.");
        if (mano == null) Debug.LogError("[Pickup] ERROR: 'mano' no asignado en Inspector.");
        if (animator == null) Debug.LogWarning("[Pickup] Aviso: 'animator' no asignado. La animaci�n no se ejecutar�.");

        if (pickup != null)
        {
            // Guardar escala y rotaci�n original
            escalaOriginal = pickup.transform.localScale;
            rotacionOriginal = pickup.transform.rotation;

            // Obtener componentes necesarios
            pickupCollider = pickup.GetComponent<Collider>();
            if (pickupCollider == null) Debug.LogError("[Pickup] ERROR: El objeto 'pickup' necesita un Collider.");

            pickupRigidbody = pickup.GetComponent<Rigidbody>();
            if (pickupRigidbody == null)
            {
                pickupRigidbody = pickup.AddComponent<Rigidbody>();
                Debug.LogWarning("[Pickup] Rigidbody agregado autom�ticamente al objeto pickup.");
            }
            // Inicializar Rigidbody
            pickupRigidbody.useGravity = true;
            pickupRigidbody.isKinematic = false;
        }

        // Buscar componente de posici�n de entrega en la escena
        PosicionObjetos = FindObjectOfType<Scr_PosicionObjetos>();
        if (PosicionObjetos == null)
            Debug.LogError("[Pickup] ERROR: No se encontr� Scr_PosicionObjetos en la escena.");
    }

    public void Update()
    {
        // Recoger con E
        if (activo && Input.GetKeyDown(KeyCode.E) && !objetoEnMano)
        {
            if (IDObjeto == 1 && PosicionObjetos != null && !PosicionObjetos.ObjetoDejado)
            {
                PillarObjeto();
                TieneElTeclado = true;
                Debug.Log("[Pickup] Teclado recogido. TieneElTeclado = " + TieneElTeclado);
            }
            else if (IDObjeto == 1 && PosicionObjetos != null && PosicionObjetos.ObjetoDejado)
            {
                Debug.Log("[Pickup] El teclado ya fue entregado. No se puede recoger.");
            }
            else
            {
                PillarObjeto();
            }
        }

        // Soltar con R
        if (Input.GetKeyDown(KeyCode.R) && objetoEnMano)
        {
            SoltarObjeto();
            Debug.Log("[Pickup] Tecla R presionada.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!objetoEnMano && other.CompareTag("Player"))
            activo = true;

        if (other.CompareTag("Comprobante"))
            SetComprobante(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!objetoEnMano && other.CompareTag("Player"))
            activo = false;

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
        if (animator != null)
            animator.SetBool("Comprobante", value);
        Debug.Log("[Pickup] Comprobante=" + Comprobante);
    }

    public void PillarObjeto()
    {
        if (pickup == null || mano == null) return;

        // Parent manteniendo escala y rotaci�n mundiales
        pickup.transform.SetParent(mano, true);
        // Colocar en la posici�n de la mano
        pickup.transform.position = mano.position;
        // Restaurar rotaci�n original
        pickup.transform.rotation = rotacionOriginal;

        // Desactivar f�sica y colisi�n
        if (pickupRigidbody != null)
            pickupRigidbody.isKinematic = true;
        if (pickupCollider != null)
            pickupCollider.isTrigger = true;

        objetoEnMano = true;
        Debug.Log("[Pickup] En mano. objetoEnMano=" + objetoEnMano);
    }

    public void SoltarObjeto()
    {
        Debug.Log("[Pickup] SoltarObjeto() invocado. PuedeDejarObjeto=" +
            (PosicionObjetos != null ? PosicionObjetos.PuedeDejarObjeto.ToString() : "SinPosicion"));

        if (IDObjeto == 1 && PosicionObjetos != null && PosicionObjetos.PuedeDejarObjeto)
            DejarTeclado();
        else
            DejarObjeto();
    }

    public void DejarObjeto()
    {
        if (pickup == null) return;

        // Desvincular manteniendo mundo
        pickup.transform.SetParent(null, true);
        // Restaurar escala y rotaci�n originales
        pickup.transform.localScale = escalaOriginal;
        pickup.transform.rotation = rotacionOriginal;

        // Reactivar f�sica y colisi�n
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

    public void DejarTeclado()
    {
        if (pickup == null) return;

        // Desvincular manteniendo mundo
        pickup.transform.SetParent(null, true);
        // Restaurar escala y rotaci�n originales
        pickup.transform.localScale = escalaOriginal;
        pickup.transform.rotation = rotacionOriginal;

        // Mover a punto de entrega
        if (PosicionObjetos != null)
        {
            pickup.transform.position = PosicionObjetos.transform.position;
            pickup.transform.rotation = PosicionObjetos.transform.rotation;
        }

        // Dejar est�tico
        if (pickupRigidbody != null)
        {
            pickupRigidbody.isKinematic = true;
            pickupRigidbody.useGravity = false;
        }
        if (pickupCollider != null)
            pickupCollider.isTrigger = false;

        objetoEnMano = false;
        activo = false;
        if (PosicionObjetos != null)
            PosicionObjetos.ObjetoDejado = true;

        Debug.Log("[Pickup] Teclado entregado. objetoEnMano=" + objetoEnMano +
            ", activo=" + activo + ", ObjetoDejado=" +
            (PosicionObjetos != null ? PosicionObjetos.ObjetoDejado.ToString() : "?"));
    }
}