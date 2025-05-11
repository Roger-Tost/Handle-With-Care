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

    [SerializeField] private AudioClip OpenDoorClip; // Sound when the door opens
    [SerializeField] private AudioSource bgMusic; // Reference to the first background music AudioSource
    [SerializeField] private AudioSource bgMusic2; // Reference to the second background music AudioSource
    public float fadeDuration = 2f; // Duration of the fade-out effect

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
        // Fade out the first background music and fade in the second one
        StartCoroutine(FadeOutMusic(bgMusic));

        // Disable buttons to prevent re-clicking
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);

        // Trigger the door animation
        if (DoorAnimator != null)
        {
            DoorAnimator.SetTrigger("Open");
            // Opening Door Sound
            Scr_SoundManager.instance.PlaySoundFXClip(OpenDoorClip, transform, 1f);
        }

        // Wait for the fade out to complete before proceeding
        yield return new WaitForSeconds(fadeDuration);

        // Start the second track and fade it in
        bgMusic2.Play();
        StartCoroutine(FadeInMusic(bgMusic2));

        // Wait for the door animation to play
        yield return new WaitForSeconds(doorOpenDelay);

        // Load the next scene
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Coroutine to fade out the background music
    private IEnumerator FadeOutMusic(AudioSource music)
    {
        float startVolume = music.volume;

        // Gradually reduce the volume to 0
        while (music.volume > 0)
        {
            music.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Stop the music once the volume reaches 0
        music.Stop();
        music.volume = startVolume; // Reset volume for potential next play
    }

    // Coroutine to fade in the second background music
    private IEnumerator FadeInMusic(AudioSource music)
    {
        music.volume = 0; // Start from silence
        float targetVolume = 1f;

        // Gradually increase the volume to target level
        while (music.volume < targetVolume)
        {
            music.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        music.volume = targetVolume; // Ensure it ends exactly at the target volume
    }
}
