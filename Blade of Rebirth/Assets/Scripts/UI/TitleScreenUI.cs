using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject titleScreen;
    [SerializeField] Scene testStage;
    [SerializeField] Scene tutorialStage;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
