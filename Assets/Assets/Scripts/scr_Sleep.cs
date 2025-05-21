using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scr_Sleep: MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public float waitTime = 1.0f;
    private bool activo;
    private scr_CharacterMovement CharacterScript;

    public bool HasSleep = false; // Nueva variable
    private Collider sleepCollider;

    void Start()
    {
        CharacterScript = FindObjectOfType<scr_CharacterMovement>();
        sleepCollider = GetComponent<Collider>();

        if (fadeImage == null)
        {
            fadeImage = FindObjectOfType<Canvas>().GetComponentInChildren<Image>();
            if (fadeImage == null)
                return;
        }

        fadeImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (activo && Input.GetKeyDown(KeyCode.E))
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(Dormir(4));
            StartCoroutine(FadeInAndOut());
            CharacterScript.CanMove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activo = false;
        }
    }

    private IEnumerator FadeInAndOut()
    {
        if (fadeImage == null) yield break;
        yield return StartCoroutine(Fade(0f, 1f));
        yield return new WaitForSeconds(waitTime);
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

        currentColor.a = endAlpha;
        fadeImage.color = currentColor;
    }

    private IEnumerator Dormir(float TiempoCama)
    {
        CharacterScript.transform.position = transform.position;
        yield return new WaitForSeconds(TiempoCama);
        CharacterScript.CanMove = true;
        HasSleep = true;

        activo = false; // Desactivamos manualmente la interacción
        sleepCollider.enabled = false; // Y luego desactivamos el collider
    }
}