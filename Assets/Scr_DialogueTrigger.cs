using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Referencia UI TMP")]
    [Tooltip("Arrastra aquí el componente TextMeshProUGUI que mostrará los diálogos")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Diálogos")]
    [Tooltip("Escribe aquí uno o varios textos; se mostrarán en orden")]
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogues;

    [Header("Ajustes")]
    [Tooltip("Tiempo (en segundos) que dura cada diálogo en pantalla")]
    [SerializeField] private float displayTime = 2f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(ShowDialogues());
        }
    }

    private IEnumerator ShowDialogues()
    {
        foreach (string dialogue in dialogues)
        {
            dialogueText.text = dialogue;
            yield return new WaitForSeconds(displayTime);
        }

        dialogueText.text = "";
        gameObject.SetActive(false);
    }
}