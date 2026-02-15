using UnityEngine;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;

public class XRTrackingReset : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Wait a few frames for XR to initialize
        yield return new WaitForSeconds(0.5f);

        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        foreach (var xr in subsystems)
        {
            if (xr.running)
            {
                xr.TryRecenter();
                Debug.Log("XR Recentered");
                break;
            }
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            StartCoroutine(RecenterDelayed());
        }
    }

    private IEnumerator RecenterDelayed()
    {
        yield return new WaitForSeconds(0.3f);

        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        foreach (var xr in subsystems)
        {
            if (xr.running)
            {
                xr.TryRecenter();
                Debug.Log("XR Recentered after focus");
                break;
            }
        }
    }
}
