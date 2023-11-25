using UnityEngine;
using UnityEngine.Events;

public class OrientationManager : MonoBehaviour
{
    public static OrientationManager Instance;
    public DeviceOrientation previousOrientation;
    public DeviceOrientation currentOrientation;
    public UnityEvent OnOrientationChanged;
    public void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    //void Update()
    //{
    //    currentOrientation = Input.deviceOrientation;
    //    if (previousOrientation != currentOrientation)
    //    {
    //        Debug.Log("Hello");
    //        OnOrientationChanged?.Invoke();
    //    }
    //    previousOrientation = currentOrientation;
    //}
}
