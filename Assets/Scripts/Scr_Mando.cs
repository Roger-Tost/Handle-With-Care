using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_Mando : MonoBehaviour
{
    
    private bool TieneElMando;
    private bool TocandoColision;

    private scr_PickupObject PickupObject;
    private scr_VideoTrigger ScriptVideo;

    private void Start()
    {
        PickupObject = FindObjectOfType<scr_PickupObject>();
        ScriptVideo = FindObjectOfType<scr_VideoTrigger>();
        TieneElMando = false;
    }

    private void Update()
    {
        if (TocandoColision)
        {
            if (Input.GetKeyDown(KeyCode.E) && PickupObject.objetoEnMano == false) // Solo permitir al jugador recoger si no tiene el objeto
            {
                PickupObject.PillarObjeto();
                TieneElMando = true;
            }
            if (Input.GetKeyDown(KeyCode.R) && PickupObject.objetoEnMano)
            {
                PickupObject.DejarObjeto();
                TieneElMando = false;
            }
        }







        if (TieneElMando)
        {
            if (Input.GetMouseButton(0))
            {
                ScriptVideo.MostrarVideo();
                Debug.Log("Encendiendo tele desde el mando");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickupObject.objetoEnMano == false)
        {
            TocandoColision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && PickupObject.objetoEnMano)
        {
            TocandoColision = false;
        }
    }
}
