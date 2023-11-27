using cakeslice;
using DG.Tweening;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public bool isAllowedToRotate;
    public GameObject ingredient;
    public Transform PizzaBox;
    private float initialROtationY, initialROtationZ;
    private Quaternion originalRotation, originalLidRotation;
    public Transform boxLidTransform;
    public Transform Root;

    void Start()
    {
        UIManager.Instance.DisableScanPanel();
        GameManager.Instance.OnGameOver.AddListener(RevealPizza);
        GameManager.Instance.OnScoreUpdated.AddListener(ShakeBox);
        isAllowedToRotate = true;
        RotateObject();
        Root.GetComponentInChildren<Outline>().color = 0;
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy) UIManager.Instance.SpawnPosition = transform.position;
        if (!isAllowedToRotate) transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));           
    }

    private void RotateObject()
    {
        transform.DOScale(new Vector3(25f, 25f, 25f), 2.5f);
        //transform.DORotate(new Vector3(0f, -90f, 0f), 2.5f)
        transform.DORotate(new Vector3(0f, 0f, 0f), 1.5f)
        .OnComplete(() => {
            isAllowedToRotate = false;
            GameManager.Instance.OnBoxScaledUp?.Invoke();
        });
    }
    
    [ContextMenu("ShakeBox")]
    public void ShakeBox()
    {
        Debug.Log("Origin: " + transform.rotation);
        Root.GetComponentInChildren<Outline>().color = 2;
        const float rotationAmount = 5f;
        const int numRotations = 5;
        const float duration = 0.1f;

        Sequence shakeSequence = DOTween.Sequence();
        originalRotation = PizzaBox.transform.rotation;
        originalLidRotation = boxLidTransform.transform.rotation;
        Debug.Log(originalRotation);


        PizzaBox.DOMoveY(PizzaBox.position.y + 0.01f, 0.5f);
        for (int i = 0; i < numRotations; i++)
        {
            shakeSequence.Append(PizzaBox.DORotate(new Vector3(originalRotation.eulerAngles.x - rotationAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z), duration));
            shakeSequence.Append(PizzaBox.DORotate(new Vector3(originalRotation.eulerAngles.x + rotationAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z), duration));
            Debug.Log("Origin: " + transform.rotation);
        }
        shakeSequence.OnComplete(() => {
            PizzaBox.DOMoveY(PizzaBox.position.y - 0.01f, 0.5f);
            PizzaBox.DORotate(originalRotation.eulerAngles, duration);
            Root.GetComponentInChildren<Outline>().color = 0;
        });
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && GameManager.Instance.CurrentIngredient != null)
        {
            Root.GetComponentInChildren<Outline>().color = 2;
            GameManager.Instance.isBoxTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Root.GetComponentInChildren<Outline>().color = 0;
            GameManager.Instance.isBoxTriggered = false;
        }
    }
    [ContextMenu("RevealPizza")]
    private void RevealPizza()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        originalLidRotation.eulerAngles = new Vector3(0,0,0);
        originalLidRotation.eulerAngles = new Vector3(0,0,90);
        //boxLidTransform.DORotate(originalLidRotation.eulerAngles, 0.25f);
        boxLidTransform.rotation = originalLidRotation;
    }
}
