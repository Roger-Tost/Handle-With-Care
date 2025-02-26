using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_PC : MonoBehaviour
{
    public Scr_SistemaOrdenador PCinterfaz;
    public bool IsOnPC = false;

    private void Start()
    {

    }


    private void Update()
    {
        if (IsOnPC && Input.GetKeyDown(KeyCode.E))
        {
            PCinterfaz.ActivarPC();

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsOnPC = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsOnPC = false;
        }
    }






}
