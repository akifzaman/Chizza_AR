using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ItemController : MonoBehaviour
{
    public GameObject target;
    public float rotationSpeed = 10.0f;
    public float val;
    public bool isAllowedToRotate;
    public GameObject ingredient;
    public void Start()
    {
        val = 360;
        RepeatIngredient();
        isAllowedToRotate = true;
        RotateObject();
        target = GameObject.FindWithTag("Target");
    }
    private void Update()
    {
        if (!isAllowedToRotate)
        {
            transform.rotation = new Quaternion(0f, 360f, 0f, 0f);
        }
    }
    private void ChangeOrientation()
    {
        if (target == null) return;
        else if (OrientationManager.Instance.currentOrientation == DeviceOrientation.Portrait)
        {
            Vector3 directionToTarget = new Vector3(-90f, 0, 0);
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        else if (OrientationManager.Instance.currentOrientation == DeviceOrientation.FaceUp)
        {
            Vector3 directionToTarget = new Vector3(-15f, 0, 0);
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        //Vector3 directionToTargetTemp = target.transform.position - transform.position;    
    }
    private void RotateObject()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 2f, RotateMode.Fast).
            OnComplete(ToggleRotationAllowance);
    }
    private void ToggleRotationAllowance()
    {
        isAllowedToRotate = false;
    }
    private void RepeatIngredient()
    {
        StartCoroutine(InstantiateIngredients());
    }
    IEnumerator InstantiateIngredients()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(ingredient, transform.position, Quaternion.identity);
        RepeatIngredient();
    }
}
