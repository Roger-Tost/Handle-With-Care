using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaLab : MonoBehaviour
{
    [SerializeField] private AudioClip OpenDoorClip;

    [Header("Animación")]
    public Animator doorAnimator;

    [Header("Estado de la puerta")]
    public bool isOpen = false;
    public bool isLocked;
    public bool isLockedWithKeyLab;

    [Header("UI")]
    [SerializeField] private GameObject interactText;
    [SerializeField] private TextMeshProUGUI missingKeyText;

    [Header("Referencia a la llave")]
    public LlaveLab keyPickup;

    [Header("Escena a cargar")]
    [SerializeField] private string sceneToLoad;

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
            if (isLockedWithKeyLab)
            {
                if (keyPickup != null && keyPickup.GetKey)
                {
                    AbrirPuerta();
                }
                else
                {
                    MostrarMensaje("Te falta una llave");
                }
            }
            else if (!isLocked)
            {
                AbrirPuerta();
            }
            else // Puerta bloqueada estándar (sin llave)
            {
                doorAnimator.SetTrigger("isLocked");
                MostrarMensaje("Está bloqueada");
            }

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

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(AbrirPuertaYEsperarCambioDeEscena());
        }
    }

    private IEnumerator AbrirPuertaYEsperarCambioDeEscena()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void MostrarMensaje(string mensaje)
    {
        if (missingKeyText != null)
        {
            missingKeyText.text = mensaje;
            missingKeyText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactText != null)
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