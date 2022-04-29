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
            case Formula.OwenWowson:
                GetOwenWilsonWow();
                break;
            case Formula.MovieSnacks:
                break;
            case Formula.FruitySoundtracks:
                break;
        }
    }

    private void GetKanyeCats()
    {
        apiController.GetKanyeResponse(result =>
        {
            apiController.GetRandomCat(response =>
            {
                apiController.GetTexture(response[0].url,
                    (texture) =>
                    {
                        mainScreen.ApplyTexture(texture);  //some of the textures are just red, reason unknown
                        mainScreen.ApplyResults(result.quote);
                    });
            });
        });
    }

    private void GetOwenWilsonWow()
    {
        apiController.GetRandomOwenQuote(result =>
        {
            apiController.GetTexture(result[0].poster,
                (texture) =>
                {
                    //can do other stuff with video and audio
                    mainScreen.ApplyTexture(texture);  //some of the textures are just red, reason unknown
                    mainScreen.ApplyResults(result[0].full_line + "\n"+result[0].movie);
                });
        });
    }
}