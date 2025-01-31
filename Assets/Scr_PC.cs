using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PC : MonoBehaviour
{
    Scr_SistemaOrdenador PCinterfaz;


    private void Start()
    {
        PCinterfaz = FindObjectOfType<Scr_SistemaOrdenador>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Time.timeScale = 0;
                PCinterfaz.ActivarPC();
            }
        }
    }


}
