using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Scr_grabadora : MonoBehaviour
{
    public GameObject interactionPromptUI;
    public string[] TextosParaMostrar;
    public AudioSource[] abuela;
    int textoActual;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] float time;
    public bool coldown = false;
    bool activo;

    // Start is called before the first frame update
    void Start()
    {

    }


    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.E) && activo && !coldown) 
        {
            if (textoActual < TextosParaMostrar.Length)
            {
                //aqui va el audio de enceder
                StartCoroutine(ShowText(TextosParaMostrar[textoActual]));

                switch (textoActual) 
                {
                    case 0: abuela[0].Play(); Debug.Log("audio 0"); break;

                    case 1: abuela[1].Play(); Debug.Log("audio 1"); break;

                    case 2: abuela[2].Play(); Debug.Log("audio 2"); break;

                    case 3: abuela[3].Play(); Debug.Log("audio 3"); break;

                    case 4: abuela[4].Play(); Debug.Log("audio 4"); break;

                }

                textoActual++;

            }

            else
            {
   
                textoActual = 0;
                StartCoroutine(ShowText(TextosParaMostrar[textoActual]));
                
                switch (textoActual) 
                {
                    case 0: abuela[0].Play(); Debug.Log("audio 0"); break;

                    case 1: abuela[1].Play(); Debug.Log("audio 1"); break;

                    case 2: abuela[2].Play(); Debug.Log("audio 2"); break;

                    case 3: abuela[3].Play(); Debug.Log("audio 3"); break;

                    case 4: abuela[4].Play(); Debug.Log("audio 4"); break;

                    case 5: abuela[4].Play(); Debug.Log("audio 5"); break;

                    case 6: abuela[6].Play(); Debug.Log("audio 6"); break;
                }

                textoActual++;

            }
        }

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activo = true;
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activo = false;
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(false);
        }

    }

    IEnumerator ShowText(string texto) 
    {
        textUI.text = texto;
        textUI.maxVisibleCharacters = 0;

        foreach (char c in texto) 
        {
            coldown = true;
            textUI.maxVisibleCharacters++;
            yield return new WaitForSeconds(time);
            
        }
        Debug.Log("finished");
        coldown = false;

        foreach (var audio in abuela)
        {
            audio.Stop();
        }
    }
}