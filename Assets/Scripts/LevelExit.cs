using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelDelay = 5f;

    void OnTriggerEnter2D(Collider2D other) 
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {

        yield return new WaitForSecondsRealtime(levelDelay);

        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = levelIndex + 1;

        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }

        FindObjectOfType<GameSession>().zeroSessionScore();

        SceneManager.LoadScene(nextLevel);
    }
}
