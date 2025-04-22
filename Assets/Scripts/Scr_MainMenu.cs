using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_MainMenu : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject QuitButton;
    public Animator DoorAnimator; // Assign this in Inspector
    public float doorOpenDelay = 2f; // Set this to match your animation length

    private bool isButtonPressed = false;

    public void PlayGame()
    {
        if (!isButtonPressed)
        {
            isButtonPressed = true;
            StartCoroutine(PlaySequence());
        }
    }

    IEnumerator PlaySequence()
    {
        // Disable buttons to prevent re-clicking
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);

        // Trigger the door animation
        if (DoorAnimator != null)
        {
            DoorAnimator.SetTrigger("Open");
        }

        // Wait for door animation to play
        yield return new WaitForSeconds(doorOpenDelay);

        // Load the next scene
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
