using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLifes : MonoBehaviour
{
    public int lives = 3;
    public Image[] livesUI;
    public GameObject explosionPrefab;
    //GameOverPanel
    public GameObject gameOverPanel;

    
    public string playerName = "DefaultPlayer";
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            HandleProjectileCollision(collision.gameObject);
        }
        else
        {
            HandleCollision(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            HandleCollision(collision.gameObject);
        }
    }

    private void HandleProjectileCollision(GameObject collidedObject)
    {
        // Destroy enemy projectile regardless
        Destroy(collidedObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        lives -= 1;

        // Update UI based on remaining lives
        for (int i = 0; i < livesUI.Length; i++)
        {
            if (i < lives)
            {
                livesUI[i].enabled = true;
            }
            else
            {
                livesUI[i].enabled = false;
            }
        }

        // Check if player has no more lives
        if (lives <= 0)
        {
            Debug.Log("GameObject Destroyed");
            Destroy(gameObject);
            Debug.Log("Game Over!");
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            // Call the HighScoreUpdate
            
        }
    }

    private void HandleCollision(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Enemy"))
        {

            Destroy(collidedObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            lives -= 1;

            // Update UI based on remaining lives
            for (int i = 0; i < livesUI.Length; i++)
            {
                livesUI[i].enabled = i < lives;
            }

            // Check if player has no more lives
            if (lives <= 0)
            {
                Debug.Log("GameObject Destroyed");
                Destroy(gameObject);
                Debug.Log("Game Over!");
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);

                // Call the HighScoreUpdate
            }
        }
    }
}