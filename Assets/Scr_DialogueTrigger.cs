using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Referencia UI TMP")]
    [Tooltip("Arrastra aqu� el componente TextMeshProUGUI que mostrar� los di�logos")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Di�logos")]
    [Tooltip("Escribe aqu� uno o varios textos; se mostrar�n en orden")]
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogues;

    [Header("Ajustes")]
    [Tooltip("Tiempo (en segundos) que dura cada di�logo en pantalla")]
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