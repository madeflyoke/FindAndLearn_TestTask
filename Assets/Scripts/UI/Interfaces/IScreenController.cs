namespace UI.Interfaces
{
    public interface IScreenController
    {
        public void ShowScreen<T>() where T : IScreen;
        
        public T GetScreen<T>() where T : IScreen;
    }
}
