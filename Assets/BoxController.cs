using DG.Tweening;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public bool isAllowedToRotate;
    public GameObject ingredient;
    public Transform PizzaBox;
    void Start()
    {
        GameManager.Instance.OnScoreUpdated.AddListener(ShakeBox);
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
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1.5f);
        transform.DORotate(new Vector3(0f, -90f, 0f), 2.5f)
        .OnComplete(() => {
            isAllowedToRotate = false;
            GameManager.Instance.OnBoxScaledUp?.Invoke();
        });
    }
    [ContextMenu("ShakeBox")]
    public void ShakeBox()
    {
        const float rotationAmount = 5f;
        const int numRotations = 5;
        const float duration = 0.1f;

        Sequence shakeSequence = DOTween.Sequence();

        Quaternion originalRotation = PizzaBox.rotation; // Store the original rotation
        PizzaBox.DOMoveY(PizzaBox.position.y + 0.01f, 0.5f);
        for (int i = 0; i < numRotations; i++)
        {
            // Rotate to the right
            shakeSequence.Append(PizzaBox.DORotate(new Vector3(originalRotation.eulerAngles.x - rotationAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z), duration));

            // Rotate back to the original position
            shakeSequence.Append(PizzaBox.DORotate(new Vector3(originalRotation.eulerAngles.x + rotationAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z), duration));
            //if(i == 3) 
        }

        // This callback will be called when the entire sequence is completed
        shakeSequence.OnComplete(() => {
            PizzaBox.DOMoveY(PizzaBox.position.y - 0.01f, 0.5f);
            PizzaBox.DORotate(originalRotation.eulerAngles, duration);
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
