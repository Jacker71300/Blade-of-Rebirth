using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject HUD;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResumeOrPause();
        }
    }

    public void ResumeOrPause()
    {
        if(!GameIsPaused)
        {
            //Go from play to pause
            HUD.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //Go from pause back to play
            pauseMenu.SetActive(false);
            HUD.SetActive(true);
            Time.timeScale = 1f;
            GameIsPaused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title Menu");
    }

    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }
}
