using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startScreen : MonoBehaviour
{

    public Button start;
    


    private void Awake()
    {
        start.onClick.AddListener(playGame);
    }

    private void playGame()
    {
        SceneManager.LoadScene(1);


    }
}