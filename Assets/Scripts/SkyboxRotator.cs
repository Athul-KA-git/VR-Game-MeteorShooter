using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }

    [Header("Skybox Rotation Settings")]
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 1f;

    public RotationDirection direction = RotationDirection.Clockwise;

    void Update()
    {
        if (RenderSettings.skybox == null)
            return;

        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");

        float dirMultiplier = (direction == RotationDirection.Clockwise) ? 1f : -1f;

        currentRotation += rotationSpeed * dirMultiplier * Time.deltaTime;
        currentRotation %= 360f;

        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
    }
}