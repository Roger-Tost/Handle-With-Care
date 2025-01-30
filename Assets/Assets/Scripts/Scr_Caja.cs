using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Caja : MonoBehaviour
{
    public Transform player;
    public GameObject textoCanvas;
    private bool PuedeAbrirCaja;
    private bool CajaAbierta;








    void Start()
    {
        PuedeAbrirCaja = false;
        CajaAbierta = false;
    }

    

    void Update()
    {
        if (PuedeAbrirCaja && !CajaAbierta && Input.GetKeyDown(KeyCode.E))
        { 
            PuedeAbrirCaja = false;
            CajaAbierta = true;
            Debug.Log("AbriendoCofre");
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (textoCanvas != null)
            {
                textoCanvas.SetActive(true);
                PuedeAbrirCaja = true;
            }
        }
    } 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (textoCanvas != null)
            {
                textoCanvas.SetActive(false);
                PuedeAbrirCaja = false;
            }
        }
    }
}
