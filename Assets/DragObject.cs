using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private bool dragging;
    private float dist;
    private Vector3 v3;
    private Vector3 offset;
    private Transform toDrag;
    public Transform ParentTransform;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            Vector3 pos = Input.GetTouch(0).position;

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                toDrag = GetIngredientAtTouchPosition(pos);

                if (toDrag != null)
                {
                    GameManager.Instance.CurrentIngredient = toDrag.gameObject;
                    dist = toDrag.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }
            else if (dragging && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (gameObject != GameManager.Instance.CurrentIngredient) return;
                transform.SetParent(null);
                v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                toDrag.position = v3 + offset;
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
        transform.position = ParentTransform.position;
        transform.SetParent(ParentTransform);

        //transform.DOMove(ParentTransform.position, 1f).
        //OnComplete(() =>
        //{
        //    transform.SetParent(ParentTransform);
        //});
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
