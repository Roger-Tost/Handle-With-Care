using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class Scr_grabadora : MonoBehaviour
{
    public GameObject interactionPromptUI;
    [SerializeField] string texttoshow;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] float time;
    bool activo;

    // Start is called before the first frame update
    void Start()
    {

    }


    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.E) && activo) 
        {
            StartCoroutine(nameof(ShowText));
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

    IEnumerator ShowText() 
    {
        textUI.text = texttoshow;
        textUI.maxVisibleCharacters = 0;

        foreach (char c in texttoshow) 
        {
            textUI.maxVisibleCharacters++;
            yield return new WaitForSeconds(time);
        }

    }

}





