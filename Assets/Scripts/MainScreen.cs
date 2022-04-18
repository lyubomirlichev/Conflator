using System;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Formula
{
    None,
    KanyeCats,
    WeatherPuns,
    MovieSnacks,
    FruitySoundtracks
}

public class MainScreen : MonoBehaviour
{
    public Action<Formula> OnFormulaSelected;
    public Action OnGenerate;
    
    [SerializeField] private Transform navigation;
    [SerializeField] private Button generateBtn;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image image;
    [SerializeField] private CanvasGroup container;
    
    public void Init()
    {
        foreach (Transform tile in navigation)
        {
            TextMeshProUGUI text = tile.GetComponentInChildren<TextMeshProUGUI>();
            tile.GetComponent<Button>().onClick.AddListener(() => { SelectFormula(text.text); });
        }

        generateBtn.onClick.AddListener(() => { OnGenerate?.Invoke(); });

        ToggleContainer(false);
        image.gameObject.SetActive(false);
    }

    private void ToggleContainer(bool state)
    {
        container.alpha = state ? 1f : 0f;
        container.blocksRaycasts = state;
        container.interactable = state;
    }
    private void SelectFormula(string formulaTitle)
    {
        var selection = Formula.None;
        if (formulaTitle == "Kanye cats")
        {
            selection = Formula.KanyeCats;
        }
        
        ToggleContainer(true);
        OnFormulaSelected?.Invoke(selection);
    }

    public void ApplyResults(string result)
    {
        label.SetText(result);
    }

    public void ApplyTexture(Texture2D texture2D)
    {
        var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0f, 0f));
        image.sprite = sprite;
        image.gameObject.SetActive(true);
    }
}