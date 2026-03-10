using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Optional UI Panels")]
    public GameObject gameOverPanel;

    // Retry the current level
    public void Retry()
    {
        // Resume time before reloading
        Time.timeScale = 1f;

        // Hide UI just in case
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit the game
    public void Quit()
    {
        Debug.Log("Quit Game");

        // Always restore time scale
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}