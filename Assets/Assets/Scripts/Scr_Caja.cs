using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Caja : MonoBehaviour
{
    public Transform player;
    private bool CajaAbierta;
    public Renderer cajaRenderer;
    public Color colorAbierto = Color.green;
    public Color colorCerrado = Color.red;

    void Start()
    {
        if (cajaRenderer == null)
        {
            cajaRenderer = GetComponent<Renderer>();
        }
        cajaRenderer.material.color = colorCerrado;
        CajaAbierta = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !CajaAbierta)
            {
                CajaAbierta = true;
                cajaRenderer.material.color = colorAbierto;
            }
        }
    }
}
