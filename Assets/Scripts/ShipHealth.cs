using System.Collections;
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
    public AudioSource hitSound;   //  Add this

    [Header("Damage Flash")]
    public CanvasGroup damageFlash;   //  Add this
    public float flashDuration = 0.2f;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Ship Health: " + health);

        if (healthUI != null)
            healthUI.UpdateHealth();

        //  Play hit sound
        if (hitSound != null)
            hitSound.Play();

        //  Camera Shake
        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(0.2f, 0.02f));

        //  Red Flash
        if (damageFlash != null)
            StartCoroutine(FlashRed());

        if (health <= 0)
        {
            GameOver();
        }
    }

    IEnumerator FlashRed()
    {
        damageFlash.alpha = 0.6f;
        yield return new WaitForSeconds(flashDuration);
        damageFlash.alpha = 0f;
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
