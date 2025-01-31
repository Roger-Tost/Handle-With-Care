using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_teclado : MonoBehaviour
{
    private Scr_PosicionObjetos PosicionObjetos;
    private scr_PickupObject PickupObjects;


    void Start()
    {
        PosicionObjetos = FindObjectOfType<Scr_PosicionObjetos>();
        PickupObjects = FindObjectOfType<scr_PickupObject>();


    }



    void Update()
    {
        







    }
}
