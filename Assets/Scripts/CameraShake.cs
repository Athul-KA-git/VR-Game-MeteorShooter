using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalLocalPos;

    private void Awake()
    {
        originalLocalPos = transform.localPosition;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalLocalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalLocalPos;
    }
}
