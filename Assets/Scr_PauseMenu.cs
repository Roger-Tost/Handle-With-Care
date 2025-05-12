using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Start()
    {
        ApplyGameplayCursorState();
    }

    void Update()
    {
        // Solo permitir pausar con Escape si el juego NO está ya en pausa
        if (Input.GetKeyDown(KeyCode.Escape) && !GameIsPaused)
        {
            Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming game...");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        StartCoroutine(ApplyGameplayCursorNextFrame());
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator ApplyGameplayCursorNextFrame()
    {
        yield return null; // esperar un frame
        ApplyGameplayCursorState();
    }

    void ApplyGameplayCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Cursor locked and hidden.");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}