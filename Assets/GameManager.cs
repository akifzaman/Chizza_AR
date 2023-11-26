using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isBoxTriggered;
    public UnityEvent OnBoxScaledUp;
    public UnityEvent OnIngredientDroppedOnBox;
    public UnityEvent OnScoreUpdated;
    public UnityEvent OnGameOver;
    public GameObject CurrentIngredient;
    public Transform RayImage;
    public ARTrackedImageManager XROrigin;
    public int score, tryCount;
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
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UpdateScore(string IngredientName)
    {
        tryCount--;
        if (UIManager.Instance.ChangeTextColor(CurrentIngredient.name.ToString()))
        {
            score += 10;
            UIManager.Instance.UpdateScoreUI(score);
        }
        if (tryCount <= 0)
        {
            OnGameOver?.Invoke();
            UIManager.Instance.OnGameOver();
        }
        OnScoreUpdated?.Invoke();
    }
    public void EnableARTracking()
    {
        XROrigin.enabled = true;
    }
}
