using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();

        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel (SceneManager.GetActiveScene().buildIndex + 1));
       
    }

    IEnumerator LoadLevel (int levelIndex)
    {

        //Play Animation
        transition.SetTrigger("Start");


        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);


    }
}
