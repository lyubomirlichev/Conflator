using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private MainScreen mainScreen;

    private APIConroller apiController;
    
    private void Start()
    {
        mainScreen.Init();

        apiController = new APIConroller();
    }
}