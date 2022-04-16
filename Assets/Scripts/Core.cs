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
                Debug.Log("success is: "+ result.quote);
            });
    }

    private void OnFormulaSelected(Formula selection)
    {
        
    }
}