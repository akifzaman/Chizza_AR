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
        if (Input.touchCount > 0 && Input.touchCount < 2)// && !IsPointerOverUIElement(Input.GetTouch(0).position))
        {
            Vector3 pos = Input.GetTouch(0).position;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(pos);
                if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Ingredient"))
                {
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }
            else if (dragging && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                GameManager.Instance.CurrentIngredient = gameObject;
                Debug.Log("akif: "+GameManager.Instance.CurrentIngredient.name);
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
    public void ResetParent()
    {
        //transform.position = ParentTransform.position;
        transform.DOMove(ParentTransform.position, 1f).
        OnComplete(() =>
        {
            transform.SetParent(ParentTransform);
        });
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

    
