using DG.Tweening;
using UnityEngine;

public class IngredientGroupController : MonoBehaviour
{
    void Start()
    {
        OrientationManager.Instance.OnOrientationChanged.AddListener(ModifyPositionAndScale);
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 15f);
    }
    public void ModifyPositionAndScale()
    {
        transform.Translate(new Vector3(-0.1f, 0.3f, 0) * Time.deltaTime);
        transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
        
    }
}
