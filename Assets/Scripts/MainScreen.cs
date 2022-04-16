using System;
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
    
    [SerializeField] private Transform navigation;
    [SerializeField] private Transform content;
    [SerializeField] private Button generateBtn;
    
    public void Init(Action onGenerate)
    {
        foreach (Transform tile in navigation)
        {
            TextMeshProUGUI text = tile.GetComponentInChildren<TextMeshProUGUI>();
            tile.GetComponent<Button>().onClick.AddListener(() => { SelectFormula(text.text); });
        }

        generateBtn.onClick.AddListener(() => { onGenerate?.Invoke(); });
    }

    private void SelectFormula(string formulaTitle)
    {
        Debug.Log("Formula selected: "+formulaTitle);
        var selection = Formula.None;
        if (formulaTitle == "Kanye cats")
        {
            selection = Formula.KanyeCats;
        }
        OnFormulaSelected?.Invoke(selection);
    }

    public void ApplyResults(string result)
    {
        Debug.Log("result is: " + result);
    }
}