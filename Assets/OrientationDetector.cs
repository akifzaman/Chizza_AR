using UnityEngine;

public class OrientationDetector : MonoBehaviour
{
    void Update()
    {
        // Check the current device orientation
        DeviceOrientation currentOrientation = Input.deviceOrientation;
        Debug.Log(currentOrientation);

        // Process the detected orientation
        switch (currentOrientation)
        {
            case DeviceOrientation.Portrait:
                Debug.Log("Device is in portrait orientation");
                break;

            case DeviceOrientation.LandscapeLeft:
            case DeviceOrientation.LandscapeRight:
                Debug.Log("Device is in landscape orientation");
                break;

            default:
                // Handle other orientations or unknown cases
                break;
        }
    }
}
