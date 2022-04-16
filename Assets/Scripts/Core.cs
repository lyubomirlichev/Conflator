using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private MainScreen mainScreen;

    private APIController apiController;

    private void Start()
    {
        mainScreen.Init(OnGenerateClick);
        mainScreen.OnFormulaSelected += OnFormulaSelected;

        apiController = new APIController();
        apiController.Init(this);
    }

    private void OnGenerateClick()
    {
        apiController.GetKanyeResponse((result) =>
        {
            mainScreen.ApplyResults(result.quote);
            
            apiController.GetRandomCat(response =>
            {
                apiController.GetCatTexture(response[0].url,
                    (texture) =>
                    {
                        mainScreen.ApplyTexture(texture);
                    });
            });
        });
    }

    private void OnFormulaSelected(Formula selection)
    {
    }
}