using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SistemaOrdenador : MonoBehaviour
{
    public GameObject InterfazPersona1;
    public GameObject InterfazPersona2;
    public GameObject InterfazPersona3;

    public GameObject PC;
    void Start()
    {
        InterfazPersona1.SetActive(false);
        InterfazPersona2.SetActive(false);
        InterfazPersona3.SetActive(false);
        PC.SetActive(false);



    }

    
    void Update()
    {
        




    }



    public void ActivarInterfaz1()
    {
        InterfazPersona1.SetActive(true);
        InterfazPersona2.SetActive(false);
        InterfazPersona3.SetActive(false);
    }
    public void ActivarInterfaz2()
    {
        InterfazPersona1.SetActive(false);
        InterfazPersona2.SetActive(true);
        InterfazPersona3.SetActive(false);
    }
    public void ActivarInterfaz3()
    {
        InterfazPersona1.SetActive(false);
        InterfazPersona2.SetActive(false);
        InterfazPersona3.SetActive(true);
    }

    public void ActivarPC()
    {
        PC.SetActive(true);
    }
}
