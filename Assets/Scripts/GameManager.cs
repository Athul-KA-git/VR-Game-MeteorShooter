using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int finalScore;   // used by GameOverScene

    public int score = 0;

    [Header("Score Display (3D Text)")]
    public TextMeshPro scoreTextFront;
    public TextMeshPro scoreTextBack;

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

    public void SaveFinalScore()
    {
        finalScore = score;
    }

    void UpdateScoreUI()
    {
        string scoreString = "SCORE              " + score;

        if (scoreTextFront != null)
            scoreTextFront.text = scoreString;

        if (scoreTextBack != null)
            scoreTextBack.text = scoreString;
    }
}