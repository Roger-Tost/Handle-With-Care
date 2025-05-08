using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scr_SistemaOrdenador : MonoBehaviour
{
    public GameObject InterfazPersona1;
    public GameObject InterfazPersona2;
    public GameObject InterfazPersona3;
    public int InterfazID;
    public int Respuesta;

    [Header ("Persona1")]
    public bool PuedeDarRespuesta1;
    public GameObject P1A;
    public GameObject P1B;
    public GameObject P1C;
    [Space(2)]
    
    [Header ("Persona2")]
    public bool PuedeDarRespuesta2;
    public GameObject P2A;
    public GameObject P2B;
    public GameObject P2C;
    [Space(2)]

    [Header ("Persona3")]
    public bool PuedeDarRespuesta3;
    public GameObject P3A;
    public GameObject P3B;
    public GameObject P3C;

    public Button[] BotonesTexto;

    public GameObject pauseMenuUI;




    public GameObject PC;
    void Start()
    {
        InterfazPersona1.SetActive(false);
        InterfazPersona2.SetActive(false);
        InterfazPersona3.SetActive(false);
        PC.SetActive(false);

        BotonesTexto[0].gameObject.SetActive(false);
        BotonesTexto[1].gameObject.SetActive(false);
        BotonesTexto[2].gameObject.SetActive(false);



    }

    
    void Update()
    {
        if (InterfazID == 1)
        {
            BotonesTexto[0].GetComponentInChildren<TextMeshProUGUI>().text = "Hey ya estoy aquí!";
            BotonesTexto[1].GetComponentInChildren<TextMeshProUGUI>().text = "Va bien, muchas cosas que ordenar :0";
            BotonesTexto[2].GetComponentInChildren<TextMeshProUGUI>().text = "Ya lo conecte lol";

        }
        else if (InterfazID == 2)
        {
            BotonesTexto[0].GetComponentInChildren<TextMeshProUGUI>().text = "Esta todo lleno de polvo";
            BotonesTexto[1].GetComponentInChildren<TextMeshProUGUI>().text = "Si, no se quien estuviera aqui pero no limpio nada";
            BotonesTexto[2].GetComponentInChildren<TextMeshProUGUI>().text = "Un poco sucia pero mejor que estar con mama";
        }
        else if (InterfazID == 3)
        {
            BotonesTexto[0].GetComponentInChildren<TextMeshProUGUI>().text = "Si, mama no me queria mas";
            BotonesTexto[1].GetComponentInChildren<TextMeshProUGUI>().text = "Ya me mude, contesto luego";
            BotonesTexto[2].GetComponentInChildren<TextMeshProUGUI>().text = "Si, estoy guardando mis cosas";
        }




    }



    public void ActivarInterfaz1()
    {
        InterfazPersona1.SetActive(true);
        InterfazPersona2.SetActive(false);
        InterfazPersona3.SetActive(false);
        InterfazID = 1;
        BotonesTexto[0].gameObject.SetActive(true);
        BotonesTexto[1].gameObject.SetActive(true);
        BotonesTexto[2].gameObject.SetActive(true);

    }
    public void ActivarInterfaz2()
    {
        InterfazPersona1.SetActive(false);
        InterfazPersona2.SetActive(true);
        InterfazPersona3.SetActive(false);
        InterfazID = 2;
        BotonesTexto[0].gameObject.SetActive(true);
        BotonesTexto[1].gameObject.SetActive(true);
        BotonesTexto[2].gameObject.SetActive(true);
    }
    public void ActivarInterfaz3()
    {
        InterfazPersona1.SetActive(false);
        InterfazPersona2.SetActive(false);
        InterfazPersona3.SetActive(true);
        InterfazID = 3;
        BotonesTexto[0].gameObject.SetActive(true);
        BotonesTexto[1].gameObject.SetActive(true);
        BotonesTexto[2].gameObject.SetActive(true);
    }

    public void ActivarPC()
    {
        PC.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

    }

    public void DesactivarPC()
    {
        PC.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;

    }

    public void Opcion(int Respuesta)
    {
        
        if (InterfazID == 1 && PuedeDarRespuesta1)
        {
            
            if (Respuesta == 1)
            {
                P1A.SetActive(true);
                PuedeDarRespuesta1 = false;
                


            }

            else if (Respuesta == 2)
            {
                P1B.SetActive(true);
                PuedeDarRespuesta1 = false;

            }

            else if (Respuesta == 3)
            { 
                P1C.SetActive(true);
                PuedeDarRespuesta1 = false;

            }
            
        }

        if (InterfazID == 2 && PuedeDarRespuesta2)
        {   
            if (Respuesta == 1)
            { 
                P2A.SetActive(true);
                PuedeDarRespuesta2 = false;
                
            }
            else if (Respuesta == 2)
            {
                P2B.SetActive(true);
                PuedeDarRespuesta2 = false;
               
            }
            else if (Respuesta == 3)
            {
                P2C.SetActive(true);
                PuedeDarRespuesta2 = false;
                
            }

        }

        if (InterfazID == 3 && PuedeDarRespuesta3)
        {
            if (Respuesta == 1)
            {
                P3A.SetActive(true);
                PuedeDarRespuesta3 = false;
                
            }
            else if (Respuesta == 2)
            {
                P3B.SetActive(true);
                PuedeDarRespuesta3 = false;

            }
            else if (Respuesta == 3)
            {
                P3C.SetActive(true);
                PuedeDarRespuesta3 = false;
               
            }

        }
    }

    



}
