using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private UIWindow uiWindowPrefab;
  
    public override void InstallBindings()
    {       
        Container.Bind<UIWindow>().FromInstance(uiWindowPrefab).AsSingle().NonLazy();
    }
}