using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PickupObject : MonoBehaviour
{
    public GameObject pickup;
    public Transform mano;
    public GameObject textoCanvas; // Referencia al texto en el Canvas
    public bool activo;
    public bool objetoEnMano; // Variable para saber si el objeto está en la mano
    public float distanciaMaxima = 3f; // Distancia máxima para interactuar
    public GameObject squareTarget; // El cuadrado invisible (debe tener el script de SquareTarget)

    private Camera cam;





    // Update is called once per frame
    void Update()
    {
        if (activo == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && !objetoEnMano) // Solo permitir al jugador recoger si no tiene el objeto
            {
                PillarObjeto();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && objetoEnMano)
        {
            DejarObjeto();
        }


        



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !objetoEnMano) // Solo activar el texto si no tienes el objeto en la mano
        {
            activo = true;
            textoCanvas.SetActive(true); // Activar el texto en el Canvas cuando el jugador entre al collider
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !objetoEnMano) // Solo desactivar el texto si no tienes el objeto en la mano
        {
            activo = false;
            textoCanvas.SetActive(false); // Desactivar el texto cuando el jugador salga del collider
        }
    }

    public void PillarObjeto()
    {
        pickup.transform.SetParent(mano);
        pickup.transform.position = mano.position;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        objetoEnMano = true;
        textoCanvas.SetActive(false); // Ocultar el texto cuando el objeto es recogido
    }

    public void DejarObjeto()
    {
        pickup.transform.SetParent(null);
        pickup.GetComponent<Rigidbody>().isKinematic = false;
        objetoEnMano = false;
    }

   

}
