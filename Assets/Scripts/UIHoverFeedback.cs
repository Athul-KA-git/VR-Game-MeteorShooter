using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class UIHoverFeedback : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip hoverSound;

    public XRBaseController leftController;
    public XRBaseController rightController;

    public float hapticAmplitude = 0.3f;
    public float hapticDuration = 0.05f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound
        if (audioSource && hoverSound)
        {
            audioSource.PlayOneShot(hoverSound);
        }

        // Haptic feedback
        if (leftController)
            leftController.SendHapticImpulse(hapticAmplitude, hapticDuration);

        if (rightController)
            rightController.SendHapticImpulse(hapticAmplitude, hapticDuration);
    }
}