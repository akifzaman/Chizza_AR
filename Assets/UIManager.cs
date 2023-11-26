using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image ChickenImage;
    public Image PizzaNameImage;
    public Image PizzaFrameImage;
    public Image StartButton;
    public void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        Sequence UIAppearanceSequence = DOTween.Sequence();
        UIAppearanceSequence.Append(StartButton.transform.DOScale(1.0f, 0.3f));
        UIAppearanceSequence.Append(PizzaFrameImage.transform.DOScale(1.0f, 0.3f));
        UIAppearanceSequence.Append(PizzaNameImage.transform.DOScale(1.0f, 0.3f));
        UIAppearanceSequence.Append(ChickenImage.transform.DOScale(1.0f, 0.3f));
    }
    public void ScaleImage(Image image, float scaleValue, float scaleDuration, UnityAction onComplete = null)
    {
        image.transform.DOScale(scaleValue, scaleDuration).
        OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}
