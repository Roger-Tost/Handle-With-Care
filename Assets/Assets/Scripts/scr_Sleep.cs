using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scr_Sleep: MonoBehaviour
{
    public GameObject textoCanvas; // Referencia al texto en el Canvas
    public Image fadeImage; // Imagen de la UI que realiza el fundido
    public float fadeDuration = 1.0f; // Duración del fundido
    public float waitTime = 1.0f; // Tiempo que la pantalla permanece en negro
    private bool activo; // Determina si el trigger está activo
    private scr_CharacterMovement CharacterScript;

    void Start()
    {
        CharacterScript = FindObjectOfType<scr_CharacterMovement>();
        // Si la imagen no está asignada manualmente, intenta buscarla automáticamente
        if (fadeImage == null)
        {
            fadeImage = FindObjectOfType<Canvas>().GetComponentInChildren<Image>();
            if (fadeImage == null)
            {
                Debug.LogError("No se encontró ninguna imagen de fundido. Asigna una manualmente.");
            }
        }
    }

    void Update()
    {
        if (activo && Input.GetKeyDown(KeyCode.E)) // Presionar 'E' dentro del trigger
        {
            StartCoroutine(Dormir(4));
            StartCoroutine(FadeInAndOut()); // Iniciar el fundido completo
            textoCanvas.SetActive(false); // Ocultar el texto
            CharacterScript.CanMove = false;
            
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que entra tiene el tag "Player"
        {
            activo = true;
            textoCanvas.SetActive(true); // Mostrar texto indicando que se puede interactuar
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que sale tiene el tag "Player"
        {
            activo = false;
            textoCanvas.SetActive(false); // Ocultar el texto al salir del trigger
        }
    }

    private IEnumerator FadeInAndOut()
    {
        if (fadeImage == null)
        {
            yield break; // Detener si no hay una imagen asignada
        }

        // Fundido a negro (fade-in)
        yield return StartCoroutine(Fade(0f, 1f));

        // Esperar mientras la pantalla está completamente negra
        yield return new WaitForSeconds(waitTime);

        // Fundido de regreso a transparente (fade-out)
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color currentColor = fadeImage.color;
        currentColor.a = startAlpha;
        fadeImage.color = currentColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            yield return null;
        }

        currentColor.a = endAlpha; // Asegurar el valor final
        fadeImage.color = currentColor;
    }


    private IEnumerator Dormir(float TiempoCama)
    {
        CharacterScript.transform.position = transform.position;
        yield return new WaitForSeconds(TiempoCama);
        CharacterScript.CanMove = true;
    }
}