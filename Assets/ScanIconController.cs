using DG.Tweening;
using UnityEngine;

public class ScanIconController : MonoBehaviour
{
    public RectTransform imageTransform;
    public float moveDistance = 200f; // Adjust this value as needed
    public float moveDuration = 2f;   // Adjust this value as needed

    void Start()
    {
        // Ensure the RectTransform component is not null
        if (imageTransform == null)
            imageTransform = GetComponent<RectTransform>();

        // Move the UI image back and forth continuously
        MoveImageContinuously();
    }

    void MoveImageContinuously()
    {
        // Move to the right
        imageTransform.DOAnchorPosX(moveDistance, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // Move to the left when the first move is complete
                imageTransform.DOAnchorPosX(-moveDistance, moveDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        // Start the process again
                        MoveImageContinuously();
                    });
            });
    }
}
