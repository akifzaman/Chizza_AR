using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnBoxScaledUp;
    public void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
