using System.Collections;
using TMPro;
using UnityEngine;

public class Scr_grabadora : MonoBehaviour
{
    public GameObject interactionPromptUI;
    public string[] TextosParaMostrar;
    public AudioSource[] abuela;

    int textoActual;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] float time = 0.05f;
    public bool coldown = false;
    bool activo;

    BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
            Debug.LogWarning("No BoxCollider found on this object.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activo && !coldown)
        {
            if (textoActual < TextosParaMostrar.Length)
            {
                StartCoroutine(PlayAudioAndShowText(textoActual));
                textoActual++;
            }
        }
    }

    IEnumerator PlayAudioAndShowText(int index)
    {
        coldown = true;

        if (index >= abuela.Length || index >= TextosParaMostrar.Length)
        {
            Debug.LogWarning("Índice fuera de rango.");
            coldown = false;
            yield break;
        }

        // Stop all other audios
        foreach (var audio in abuela)
        {
            if (audio.isPlaying) audio.Stop();
        }

        AudioSource currentAudio = abuela[index];
        string currentText = TextosParaMostrar[index];

        currentAudio.Play();
        textUI.text = currentText;
        textUI.maxVisibleCharacters = 0;

        int visibleCount = 0;

        while (currentAudio.isPlaying && visibleCount < currentText.Length)
        {
            textUI.maxVisibleCharacters++;
            visibleCount++;
            yield return new WaitForSeconds(time);
        }

        // Show remaining characters instantly if audio ends early
        textUI.maxVisibleCharacters = currentText.Length;

        // Wait until the audio ends fully
        while (currentAudio.isPlaying)
        {
            yield return null;
        }

        coldown = false;

        // Check if this was the last one
        if (textoActual >= TextosParaMostrar.Length)
        {
            EndInteractionForever();
        }
    }

    void EndInteractionForever()
    {
        activo = false;
        if (interactionPromptUI != null)
            interactionPromptUI.SetActive(false);

        if (textUI != null)
        {
            textUI.text = "";
            textUI.maxVisibleCharacters = 0;
        }

        if (boxCollider != null)
            boxCollider.enabled = false;

        Debug.Log("Interaction permanently disabled.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textoActual < TextosParaMostrar.Length)
        {
            activo = true;
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activo = false;
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(false);
        }
    }
}
