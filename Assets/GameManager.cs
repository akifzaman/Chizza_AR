using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
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
    public int score;
    public void Awake()
    {   
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void Start()
    {
        isBoxTriggered = false;
        OnIngredientDroppedOnBox.AddListener(() => UpdateScore(CurrentIngredient.name));
    }
    public void Update()
    {
        RayImage.RotateAround(RayImage.position, Vector3.forward * Time.deltaTime, -0.3f);
    }
    public void UpdateScore(string IngredientName)
    {
        Debug.Log(CurrentIngredient.name +"  "+ CurrentIngredient.name.ToString());
        if (UIManager.Instance.ChangeTextColor(CurrentIngredient.name.ToString()))
        {
            score += 10;
            UIManager.Instance.UpdateScoreUI(score);
            Debug.Log("Score: " + score);
        }
        OnScoreUpdated?.Invoke();
    }
    public void EnableARTracking()
    {
        XROrigin.enabled = true;
    }
}
