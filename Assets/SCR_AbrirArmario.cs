using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_AbrirArmario : MonoBehaviour
{
    public Animator animador;
    public TextMeshProUGUI mensajeTMP;
    public string mensajeInteraccion = "Presiona F para abrir";

    private bool jugadorCerca = false;
    private bool abierto = false;

    public GameObject Interior;

    public scr_Sleep sleepScript; // Referencia al script Sleep

    private void Start()
    {
        if (mensajeTMP != null)
            mensajeTMP.text = "";
        Interior.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !abierto && sleepScript != null && sleepScript.HasSleep)
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
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F) && !abierto && sleepScript != null && sleepScript.HasSleep)
        {
            Interior.SetActive(true);
            animador.SetBool("Abierto", true);
            abierto = true;
            if (mensajeTMP != null)
                mensajeTMP.text = "";
        }
    }
}