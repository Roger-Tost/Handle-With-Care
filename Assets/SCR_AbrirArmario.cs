using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_AbrirArmario : MonoBehaviour
{
    public Animator animador; // Asigna el Animator del objeto
    public TextMeshProUGUI mensajeTMP; // Asigna el texto TMP de la UI
    public string mensajeInteraccion = "Presiona F para abrir";

    private bool jugadorCerca = false;
    private bool abierto = false; // Controla si ya se abrió

    public GameObject Interior;

    private void Start()
    {
        if (mensajeTMP != null)
            mensajeTMP.text = ""; // Oculta el texto al inicio
        Interior.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !abierto)
        {
            jugadorCerca = true;
            if (mensajeTMP != null)
                mensajeTMP.text = mensajeInteraccion;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (mensajeTMP != null)
                mensajeTMP.text = "";
        }
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F) && !abierto)
        {
            Interior.SetActive(true);
            animador.SetBool("Abierto", true);
            abierto = true; // Marcar como abierto
            if (mensajeTMP != null)
                mensajeTMP.text = ""; // Oculta el mensaje permanentemente
        }
    }
}