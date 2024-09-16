using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIWindow : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
   
    private ISceneController _sceneController;
  
    private const string gameSceneName = "Survivour";

    [Inject]
    public void Construct(ISceneController sceneController)
    {
        _sceneController = sceneController;       
    }

    public void Awake()
    {
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _sceneController.LoadSceneAsync(gameSceneName);
    }
}
