public interface IUIFactory : IService
{
    void CreateMainMenu();
    void CreateWinWindow(string sceneKey);
    void CreateFailWindow(string sceneKey, Player player);
    void CreateHud(bool isDesktop, string sceneKey, Player player);
}
