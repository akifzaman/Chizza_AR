using DG.Tweening;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public bool isAllowedToRotate;
    public GameObject ingredient;
    
    void Start()
    {
        isAllowedToRotate = true;
        RotateObject();
    }

    private void Update()
    {
        if (!isAllowedToRotate)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));    
        }
    }

    private void RotateObject()
    {
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 2f);
        transform.DORotate(new Vector3(0f, -90f, 0f), 2.5f)
            .OnComplete(() => {
                isAllowedToRotate = false;
                GameManager.Instance.OnBoxScaledUp?.Invoke();
            });
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ingredient")) GameManager.Instance.isBoxTriggered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ingredient")) GameManager.Instance.isBoxTriggered = false;
    }
}
