using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    private void Awake()
    {
        // Singleton Setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ensure game runs normally at start
        Time.timeScale = 1f;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            Time.timeScale = 1f;
    }

    // =========================
    // SCORE SYSTEM
    // =========================

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}