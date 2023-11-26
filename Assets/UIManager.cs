using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image ChickenImage;
    public Image PizzaNameImage;
    public Image PizzaFrameImage;
    public Image StartButton;
    public Transform ScanPanel;
    public TextMeshProUGUI MeatText, BreadText, CheeseText;
    public GameObject Pizza, ParticleObject, ConfettiParticles;
    public Vector3 SpawnPosition;
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
    public void UpdateScoreUI(int value)
    {
        Debug.Log(value);
    }
    public void DisableScanPanel()
    {
        ScanPanel.gameObject.SetActive(false);
    }
    public bool ChangeTextColor(string textName)
    {
        if (textName == "Meat")
        {
            MeatText.color = Color.black;
            return true;
        }
        else if (textName == "Bread")
        {
            BreadText.color = Color.black;
            return true;
        }
        else if (textName == "Cheese")
        {
            CheeseText.color = Color.black;
            return true;
        }
        return false;
    }
    [ContextMenu("GameOver")]
    public void OnGameOver()
    {
        ParticleObject.transform.position = SpawnPosition;
        ParticleObject.SetActive(true);
        ConfettiParticles.transform.position = SpawnPosition;
        ConfettiParticles.SetActive(true);
        Pizza.transform.position = SpawnPosition;
        Pizza.transform.DOScale(0.15f, 1f);
    }
}
