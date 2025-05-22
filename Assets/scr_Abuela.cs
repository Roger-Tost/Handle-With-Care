using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scr_Abuela : MonoBehaviour
{
    public AudioSource audioGrandma;
    public AudioSource audioPlayer;
    public AudioClip Grandma_Cough;
    public AudioClip Player_Cough;
    public float delayBetweenAudios = 2f;

    public Image fadeImage; // UI negra que cubre la pantalla
    public float fadeDuration = 1f;

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

        // Fundido a negro
        yield return StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
