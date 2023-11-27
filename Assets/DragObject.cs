using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private bool dragging;
    private float dist;
    private Vector3 offset;
    private Transform toDrag;
    public Transform ParentTransform;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                toDrag = GetIngredientAtTouchPosition(touchPosition);

                if (toDrag != null)
                {
                    GameManager.Instance.CurrentIngredient = toDrag.gameObject;
                    dist = toDrag.position.z - Camera.main.transform.position.z;
                    offset = toDrag.position - Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, dist));
                    dragging = true;
                }
            }
            else if (dragging && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (gameObject != GameManager.Instance.CurrentIngredient) return;

                Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, dist)) + offset;
                toDrag.position = Vector3.Lerp(toDrag.position, newPosition, Time.deltaTime * 10f); // Adjust the speed as needed
            }
            else if (dragging && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
            {
                dragging = false;

                if (GameManager.Instance.isBoxTriggered)
                {
                    GameManager.Instance.OnIngredientDroppedOnBox?.Invoke();
                    GameManager.Instance.isBoxTriggered = false;
                    Destroy(GameManager.Instance.CurrentIngredient);
                }
                else ResetParent();
            }
        }
    }

    private Transform GetIngredientAtTouchPosition(Vector3 touchPosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Ingredient"))
        {
            return hit.transform;
        }

        return null;
    }

    public void ResetParent()
    {
        transform.position = ParentTransform.position;// Vector3.Lerp(transform.position, ParentTransform.position, Time.deltaTime * 10f); // Adjust the speed as needed
        transform.SetParent(ParentTransform);
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
