using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections.Generic;

public class ShipHealth : MonoBehaviour
{
    public int health = 20;

    [Header("UI")]
    public ShipHealthUI healthUI;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreText;

    [Header("XR UI")]
    public XRRayInteractor rightRayInteractor;
    public XRRayInteractor leftRayInteractor;

    [Header("Haptics")]
    [Range(0f, 1f)] public float hapticIntensity = 0.7f;
    public float hapticDuration = 0.15f;

    [Header("Effects")]
    public CameraShake cameraShake;
    public AudioSource hitSound;
    public DamageVolumeController damageVolume;

    private bool isPaused = false;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver && Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    void PauseGame()
    {
        isPaused = true;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (rightRayInteractor != null)
            rightRayInteractor.enabled = true;

        if (leftRayInteractor != null)
            leftRayInteractor.enabled = true;

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (rightRayInteractor != null)
            rightRayInteractor.enabled = false;

        if (leftRayInteractor != null)
            leftRayInteractor.enabled = false;

        Time.timeScale = 1f;
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver) return;

        health -= damage;

        if (healthUI != null)
            healthUI.UpdateHealth();

        if (hitSound != null)
            hitSound.Play();

        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(0.2f, 0.02f));

        if (damageVolume != null)
            damageVolume.TriggerDamage();

        TriggerHaptics();

        if (health <= 0)
            GameOver();
    }

    void TriggerHaptics()
    {
        SendHaptic(XRNode.LeftHand);
        SendHaptic(XRNode.RightHand);
    }

    void SendHaptic(XRNode node)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(node, devices);

        foreach (var device in devices)
        {
            if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities)
                && capabilities.supportsImpulse)
            {
                device.SendHapticImpulse(0, hapticIntensity, hapticDuration);
            }
        }
    }

    void GameOver()
    {
        isGameOver = true;

        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverScoreText != null && GameManager.Instance != null)
            gameOverScoreText.text = "Final Score: " + GameManager.Instance.score;

        if (rightRayInteractor != null)
            rightRayInteractor.enabled = true;

        if (leftRayInteractor != null)
            leftRayInteractor.enabled = true;
    }
}