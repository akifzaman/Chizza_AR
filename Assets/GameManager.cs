using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isBoxTriggered;
    public UnityEvent OnBoxScaledUp;
    public UnityEvent OnIngredientDroppedOnBox;
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
    public void UpdateScore()
    {
        Debug.Log("Scored");
    }
}
