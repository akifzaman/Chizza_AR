using DG.Tweening;
using System.Collections;
using UnityEngine;

public class IngredientMovementController : MonoBehaviour
{
    private Vector3 translationVector;
    private float speedModifier = 0.00020f;
    public Transform ParentTransform;
    public Vector3 initialPosition;

    public void Update()
    {
        Debug.Log(ParentTransform.position);
    }
    public void MoveItem(Touch touch)
    {
        transform.SetParent(null);
        translationVector = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        gameObject.transform.Translate(translationVector * -Input.GetTouch(0).deltaPosition.x * speedModifier, Space.World);

        translationVector = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        gameObject.transform.Translate(translationVector * Input.GetTouch(0).deltaPosition.y * speedModifier, Space.World);
    }
    [ContextMenu("ResetParent")]
    public void ResetParent()
    {
        transform.position = ParentTransform.position;
        //transform.rotation = ParentTransform.rotation;
        transform.SetParent(ParentTransform);      
    }
    public void OnItemSelect()
    {
        
    }
    public void OnItemDeselect()
    {
        
    }
}