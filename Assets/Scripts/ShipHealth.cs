using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ShipHealth : MonoBehaviour
{
    public int health = 20;

    [Header("UI")]
    public ShipHealthUI healthUI;
    public GameObject gameOverPanel;

    [Header("XR UI")]
    public XRRayInteractor rightRayInteractor;
    public XRRayInteractor leftRayInteractor;

    [Header("Effects")]
    public CameraShake cameraShake;
    public AudioSource hitSound;

    [Header("Post Processing")]
    public DamageVolumeController damageVolume;   //  New Global Volume Controller

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Ship Health: " + health);

        // Update Health Bar
        if (healthUI != null)
            healthUI.UpdateHealth();

        // Play Hit Sound
        if (hitSound != null)
            hitSound.Play();

        // Camera Shake
        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(0.2f, 0.02f));

        // Trigger URP Vignette Damage Flash
        if (damageVolume != null)
            damageVolume.TriggerDamage();

        if (health <= 0)
            GameOver();
    }

    void GameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (rightRayInteractor != null)
            rightRayInteractor.enabled = true;

        if (leftRayInteractor != null)
            leftRayInteractor.enabled = true;

        Time.timeScale = 0f;
    }
}
