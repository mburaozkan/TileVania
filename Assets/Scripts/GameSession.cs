using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int coins = 0;
    [SerializeField] TextMeshProUGUI livesText;
    
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;
    int scoreCurrentSession = 0;
    
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        livesText.text = playerLives.ToString();  
        scoreText.text = score.ToString();  
    }

    public void pickUpCoin()
    {
        coins++;
        score += 100;
        scoreCurrentSession += 100;
        scoreText.text = score.ToString();  
    }

    public void killScore()
    {
        score += 50;
        scoreCurrentSession += 50;
        scoreText.text = score.ToString(); 
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void zeroSessionScore()
    {
        scoreCurrentSession = 0;
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();

        score -= scoreCurrentSession;
        zeroSessionScore();
        scoreText.text = score.ToString();
        
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
