using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;
    public GameObject currentGameObject;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion
    private void Update()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)// && !IsPointerOverUIElement(Input.GetTouch(0).position))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Ingredient") && currentGameObject == null)
                    {
                        currentGameObject = raycastHit.collider.gameObject;
                        //currentGameObject.GetComponent<IngredientMovementController>().OnItemSelect();
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && currentGameObject != null)
            {
                currentGameObject.GetComponent<IngredientMovementController>().MoveItem(Input.GetTouch(0));
            }
        }
        else if(Input.touchCount <= 0 && currentGameObject != null)
        {
            currentGameObject.GetComponent<IngredientMovementController>().ResetParent();
            currentGameObject = null;
        }
    }
    // Helper function to check if a UI element is at a specific position
    public bool IsPointerOverUIElement(Vector2 position)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}