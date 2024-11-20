namespace UI.Interfaces.Popups
{
    public interface IPopupBase
    {
        string Key { get; }
        
        void Show();
        
        void Close();
    }
}