using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startScreen : MonoBehaviour
{

    public Button start;
    public Button exit;


    private void Awake()
    {
        start.onClick.AddListener(playGame);
        exit.onClick.AddListener(ExitGame);
    }

    private void playGame()
    {
        SceneManager.LoadScene(1);


    }

    private void ExitGame()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}