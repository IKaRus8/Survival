using UnityEngine;
using UnityEngine.UI;
using Zenject;

//to play button
public class UIWindow : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
   
    private ISceneLoader _sceneLoader;
  
    private const string gameSceneName = "Survivour";

    [Inject]
    public void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;       
    }

    public void Awake()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _startGameButton.onClick.RemoveAllListeners();
        
        _sceneLoader.LoadSceneAsync(gameSceneName);
    }
}
