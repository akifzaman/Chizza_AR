using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public bool isAllowedToRotate;
    public GameObject ingredient;

    void Start()
    {
        isAllowedToRotate = true;
        //RepeatIngredient();
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
            .OnComplete(() => isAllowedToRotate = false);
    }

    private void RepeatIngredient()
    {
        StartCoroutine(InstantiateIngredients());
    }

    IEnumerator InstantiateIngredients()
    {
        yield return new WaitForSeconds(2.5f);
        Instantiate(ingredient, transform.position, Quaternion.identity);
        RepeatIngredient();
    }
}
