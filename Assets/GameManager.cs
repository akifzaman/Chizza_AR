using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isBoxTriggered;
    public UnityEvent OnBoxScaledUp;
    public UnityEvent OnIngredientDroppedOnBox;
    public UnityEvent OnScoreUpdated;
    public GameObject CurrentIngredient;
    public Transform RayImage;
    public ARTrackedImageManager XROrigin;
    public void Awake()
    {   
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void Start()
    {
        isBoxTriggered = false;
        OnIngredientDroppedOnBox.AddListener(UpdateScore);
    }
    public void Update()
    {
        RayImage.RotateAround(RayImage.position, Vector3.forward * Time.deltaTime, -0.3f);
    }
    public void UpdateScore()
    {
        Debug.Log("Scored");
        OnScoreUpdated?.Invoke();
    }
    public void EnableARTracking()
    {
        XROrigin.enabled = true;
    }
}
