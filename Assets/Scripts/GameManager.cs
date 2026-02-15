using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            Time.timeScale = 1f;
    }
}
