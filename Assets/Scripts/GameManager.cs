using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    [Header("Score Display (3D Text)")]
    public TextMeshPro scoreText3D;   // 3D TMP (NOT UGUI)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 1f;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText3D != null)
        {
            scoreText3D.text = "Score: " + score;
        }
    }
}