using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int maxLives = 3; //3 health 
    [SerializeField] private List<Image> lifeImages;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private CoinManager coinManager;
    private int currentLives;

    void Start()
    {
        gameOverMenu.SetActive(false);
        currentLives = maxLives;

        foreach (Image image in lifeImages)
        {
            image.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            LoseLife();
            Destroy(other.gameObject);
        }
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            lifeImages[currentLives].enabled = false;

            if (currentLives == 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOverMenu.SetActive(true);
        // Additional game over logic
        Time.timeScale = 0f;

       
    }

    public void ResetGame()
    {
      
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}