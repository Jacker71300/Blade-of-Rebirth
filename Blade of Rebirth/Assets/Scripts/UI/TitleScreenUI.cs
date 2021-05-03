using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject menuButtons;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject charSelect;
    [SerializeField] GameObject settings;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
        menu.SetActive(false);
        levelSelect.SetActive(false);
        charSelect.SetActive(false);
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterGame()
    {
        Debug.Log("Enter");
        titleScreen.SetActive(false);
        menu.SetActive(true);
    }

    public void loadTestStage()
    {
        Debug.Log("Test Stage Loaded");
        SceneManager.LoadScene("Test Environment");
    }

    public void loadTutorialStage()
    {
        Debug.Log("Tutorial Stage Loaded");
        SceneManager.LoadScene("Tutorial");
    }

    public void enterLevelSelect()
    {
        menuButtons.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void exitLevelSelect()
    {
        levelSelect.SetActive(false);
        menuButtons.SetActive(true);
    }

    public void enterCharSelect()
    {
        menuButtons.SetActive(false);
        charSelect.SetActive(true);
    }

    public void exitCharSelect()
    {
        charSelect.SetActive(false);
        menuButtons.SetActive(true);
    }

    public void enterSettings()
    {
        menuButtons.SetActive(false);
        settings.SetActive(true);
    }

    public void exitSettings()
    {
        settings.SetActive(false);
        menuButtons.SetActive(true);
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
