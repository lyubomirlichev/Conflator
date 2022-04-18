using System;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private MainScreen mainScreen;

    private APIController apiController;
    private Formula currentSelection = Formula.None;

    private void Start()
    {
        mainScreen.Init();
        mainScreen.OnFormulaSelected += OnFormulaSelected;
        mainScreen.OnGenerate += OnGenerateClick;

        apiController = new APIController();
        apiController.Init(this);
    }

    private void OnGenerateClick()
    {
        OnFormulaSelected(currentSelection);
    }

    private void OnFormulaSelected(Formula selection)
    {
        currentSelection = selection;
        switch (currentSelection)
        {
            case Formula.None:
                break;
            case Formula.KanyeCats:
                GetKanyeCats();
                break;
            case Formula.WeatherPuns:
                break;
            case Formula.MovieSnacks:
                break;
            case Formula.FruitySoundtracks:
                break;
        }
    }

    private void GetKanyeCats()
    {
        apiController.GetKanyeResponse((result) =>
        {
            apiController.GetRandomCat(response =>
            {
                apiController.GetCatTexture(response[0].url,
                    (texture) =>
                    {
                        mainScreen.ApplyTexture(texture);  //some of the textures are just red, reason unknown
                        mainScreen.ApplyResults(result.quote);
                    });
            });
        });
    }
}