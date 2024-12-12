using Logic.Interfaces.Services.Level;

namespace Logic.Unity.SceneObjects
{
    public class DefendLevelSceneObjectsContainer : BaseSceneObjectsContainer, IDefendLevelSceneObjectContainer
    {
        protected override void Awake()
        {
            base.Awake();

            IBaseSceneObjectContainer.PopupContainer = _popupContainer;
        }
    }
}