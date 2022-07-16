using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    public Button restart;
    
    public Button mainMenu;
    private void Awake()
    {
        restart.onClick.AddListener(restartGame);
        mainMenu.onClick.AddListener(mainMenuScene);

    }
    public void playerDead()
    {
        gameObject.SetActive(true);


    }
    private void restartGame()
    {
        int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneNumber);
        
    }
    private void mainMenuScene()
    {
        SceneManager.LoadScene(0);
        

    }
}
