using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    public Button restart;
    
    public Button mainMenu;

    [SerializeField] Score Player1Score;
    [SerializeField] Score Player2Score;
    [SerializeField] GameObject p1Win;
    [SerializeField] GameObject p2Win;
    [SerializeField] GameObject bAudio;
    [SerializeField] GameObject gameOverAudio;

    int p1Score;
    int p2Score;


    private void Awake()
    {
        restart.onClick.AddListener(restartGame);
        mainMenu.onClick.AddListener(mainMenuScene);

    }
    public void playerDead()
    {
        gameObject.SetActive(true);

        p1Score = Player1Score.GetActiveScore();
        p2Score = Player2Score.GetActiveScore();
        if (p1Score > p2Score)
        {
            p1Win.SetActive(true);
        }
        else
        {
            p2Win.SetActive(true);
        }
        bAudio.SetActive(false);
        gameOverAudio.SetActive(true);
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
