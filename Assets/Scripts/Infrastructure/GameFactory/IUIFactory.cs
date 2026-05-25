public interface IUIFactory : IService
{
    void CreateMainMenu();
    void CreateWinWindow(LevelData levelData);
    void CreateFailWindow(LevelData levelData, Player player);
    void CreateHud(bool isDesktop, LevelData levelData, Player player);
}
