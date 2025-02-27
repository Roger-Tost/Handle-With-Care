using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PickupObject : MonoBehaviour
{
    public GameObject pickup;
    public Transform mano;
    public bool activo;
    public bool objetoEnMano;
    public float distanciaMaxima = 3f;
    public GameObject squareTarget;

    private Scr_PosicionObjetos PosicionObjetos;
    private Scr_teclado teclado;

    public int IDObjeto;
    public bool TieneElTeclado = false;

    private void Start()
    {
        PosicionObjetos = FindObjectOfType<Scr_PosicionObjetos>();
        teclado = FindObjectOfType<Scr_teclado>();
    }

    void Update()
    {
        if (activo && Input.GetKeyDown(KeyCode.E) && !objetoEnMano)
        {
            if (IDObjeto == 1 && !PosicionObjetos.ObjetoDejado)
            {
                PillarObjeto();
                TieneElTeclado = true;
            }
            else if (IDObjeto == 1 && PosicionObjetos.ObjetoDejado)
            {
                Debug.Log("Objeto ya dejado, no puedes recogerlo.");
            }
            else
            {
                PillarObjeto();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && objetoEnMano)
        {
            SoltarObjeto();
            Debug.Log("Tecla R presionada");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !objetoEnMano)
        {
            activo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !objetoEnMano)
        {
            activo = false;
        }
    }

    public void PillarObjeto()
    {
        pickup.transform.SetParent(mano);
        pickup.transform.localPosition = Vector3.zero;
        pickup.transform.localRotation = Quaternion.identity;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        objetoEnMano = true;
    }

    public void SoltarObjeto()
    {
        if (PosicionObjetos.PuedeDejarObjeto)
        {
            DejarTeclado();
        }
        else
        {
            DejarObjeto();
        }
    }

    public void DejarObjeto()
    {
        pickup.transform.SetParent(null);
        Rigidbody rb = pickup.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        objetoEnMano = false;

        // Aplicar una pequeña fuerza para dar realismo
        rb.AddForce(mano.forward * 0.5f, ForceMode.Impulse);
    }

    public void DejarTeclado()
    {
        pickup.transform.SetParent(null);
        pickup.transform.position = PosicionObjetos.transform.position;
        pickup.transform.rotation = PosicionObjetos.transform.rotation;

        Rigidbody rb = pickup.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        objetoEnMano = false;
        PosicionObjetos.ObjetoDejado = true;
        Debug.Log("Dejando el teclado en su posición");
    }
}