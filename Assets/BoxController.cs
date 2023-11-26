using cakeslice;
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
        transform.GetComponentInChildren<Outline>().color = 0;
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
        transform.GetComponentInChildren<Outline>().color = 0;
        const float rotationAmount = 5f;
        const int numRotations = 5;
        const float duration = 0.1f;

        Sequence shakeSequence = DOTween.Sequence();

        Quaternion originalRotation = PizzaBox.rotation;
        PizzaBox.DOMoveY(PizzaBox.position.y + 0.01f, 0.5f);
        for (int i = 0; i < numRotations; i++)
        {
            shakeSequence.Append(PizzaBox.DORotate(new Vector3(originalRotation.eulerAngles.x - rotationAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z), duration));
            shakeSequence.Append(PizzaBox.DORotate(new Vector3(originalRotation.eulerAngles.x + rotationAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z), duration));
        }
        shakeSequence.OnComplete(() => {
            PizzaBox.DOMoveY(PizzaBox.position.y - 0.01f, 0.5f);
            PizzaBox.DORotate(originalRotation.eulerAngles, duration);
        });
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && GameManager.Instance.CurrentIngredient != null)
        {
            transform.GetComponentInChildren<Outline>().color = 2;
            GameManager.Instance.isBoxTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            transform.GetComponentInChildren<Outline>().color = 0;
            GameManager.Instance.isBoxTriggered = false;
        }
    }
}
