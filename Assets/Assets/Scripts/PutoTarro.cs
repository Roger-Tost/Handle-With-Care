using UnityEngine;
using TMPro;

public class ActivarObjetoConE : MonoBehaviour
{
    public GameObject objetoParaActivar;         // Objeto que se activa con E
    public TextMeshProUGUI textoInteraccion;     // Texto TMP para mostrar al jugador
    public GameObject objetoActivarTrasDormir;   // Objeto que se activa cuando HasSleep == true
    public scr_Sleep sleepScript;                // Referencia al script de dormir

    private bool jugadorEnRango = false;
    private bool yaActivadoPorSueño = false;

    void Start()
    {
        if (textoInteraccion != null)
            textoInteraccion.gameObject.SetActive(false);

        if (sleepScript == null)
            sleepScript = FindObjectOfType<scr_Sleep>(); // O puedes asignarlo manualmente
    }

    void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (objetoParaActivar != null)
            {
                objetoParaActivar.SetActive(true);
                if (textoInteraccion != null)
                    textoInteraccion.gameObject.SetActive(false);
            }
        }

        // Activar objeto una sola vez cuando HasSleep sea true
        if (sleepScript != null && sleepScript.HasSleep && !yaActivadoPorSueño)
        {
            yaActivadoPorSueño = true; // Evita múltiples activaciones
            if (objetoActivarTrasDormir != null)
            {
                objetoActivarTrasDormir.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;

            if (textoInteraccion != null)
            {
                textoInteraccion.text = "Pulsa E para interactuar con EL TARRO";
                textoInteraccion.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;

            if (textoInteraccion != null)
            {
                textoInteraccion.gameObject.SetActive(false);
            }
        }
    }
}