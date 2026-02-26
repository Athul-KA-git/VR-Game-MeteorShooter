using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class DamageVolumeController : MonoBehaviour
{
    public Volume volume;
    public float maxIntensity = 0.5f;
    public float flashDuration = 0.3f;

    private Vignette vignette;
    private Coroutine flashRoutine;

    void Start()
    {
        volume.profile.TryGet(out vignette);
        vignette.intensity.value = 0f;
    }

    public void TriggerDamage()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / flashDuration;
            vignette.intensity.value = Mathf.Lerp(maxIntensity, 0f, t);
            yield return null;
        }

        vignette.intensity.value = 0f;
    }
}
