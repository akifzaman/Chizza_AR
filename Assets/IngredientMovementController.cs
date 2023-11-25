using DG.Tweening;
using System.Collections;
using UnityEngine;

public class IngredientMovementController : MonoBehaviour
{
    private Vector3 translationVector;
    private float speedModifier = 0.00020f;
    public Transform ParentTransform;
    public Vector3 initialPosition;
    private bool isChild = true;

    public void MoveItem(Touch touch)
    {
        if (isChild)
        {
            isChild = false;
            transform.SetParent(null);
        }
        translationVector = new Vector3(Camera.main.transform.forward.x * Input.GetTouch(0).deltaPosition.x * 10 * speedModifier, 0, 
            Camera.main.transform.forward.z * Input.GetTouch(0).deltaPosition.y * speedModifier);
        gameObject.transform.Translate(translationVector, Space.World);

        //translationVector = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        //gameObject.transform.Translate(translationVector * Input.GetTouch(0).deltaPosition.x * speedModifier, Space.World);
    }
    //public void MoveItem(Touch touch)
    //{
    //    if (isChild)
    //    {
    //        isChild = false;
    //        transform.SetParent(null);
    //        initialPosition = transform.position;
    //    }

    //    // Calculate the movement vector based on the touch delta position
    //    Vector3 deltaPosition = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y) * speedModifier;

    //    // Move the object based on the calculated delta position
    //    transform.position = initialPosition + Camera.main.transform.TransformDirection(deltaPosition);
    //}



    [ContextMenu("ResetParent")]
    public void ResetParent()
    {
        isChild = true;
        transform.position = ParentTransform.position;
        transform.SetParent(ParentTransform);
    }
}