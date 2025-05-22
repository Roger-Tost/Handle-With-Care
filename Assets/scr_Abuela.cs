using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_Abuela : MonoBehaviour
{
    public AudioSource audioGrandma;
    public AudioSource audioPlayer;
    public AudioClip Grandma_Cough;
    public AudioClip Player_Cough;
    public float delayBetweenAudios = 2f;
    public float longDelayBetweenAudios = 5f;

    public GameObject interactionPromptUI;
    public Image fadeImage; // UI negra que cubre la pantalla
    public float fadeDuration = 1f;
    public float waitTime = 1f;

    private bool playerInside = false;
    private bool hasInteracted = false;

    void Update()
    {
        if (playerInside && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted = true;
            StartCoroutine(PlaySequence());
        }
    }

    IEnumerator PlaySequence()
    {
        // Reproducir primer audio
        audioGrandma.clip = Grandma_Cough;
        audioGrandma.Play();

        // Esperar duración del audio o un delay fijo
        yield return new WaitForSeconds(delayBetweenAudios);

        // Reproducir segundo audio
        audioPlayer.clip = Player_Cough;
        audioPlayer.Play();

        yield return new WaitForSeconds(longDelayBetweenAudios);
        StartCoroutine(FadeInAndOut());

        yield return new WaitForSeconds(longDelayBetweenAudios);
        SceneManager.LoadScene(0);
    }
    

    private IEnumerator FadeInAndOut()
    {
        if (fadeImage == null) yield break;
        yield return StartCoroutine(Fade(0f, 1f));
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(true);
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(false);
            playerInside = false;
        }
    }
}
