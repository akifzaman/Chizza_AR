using DG.Tweening;
using UnityEngine;

public class IngredientGroupController : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnBoxScaledUp.AddListener(ModifyIngredientGroupPositionAndScale);
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 15f);
    }
    private void ModifyIngredientGroupPositionAndScale()
    {
        transform.Translate(new Vector3(-0.1f, 0.3f, 0) * Time.deltaTime);
        transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
        
    }
}
