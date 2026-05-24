public interface IUIFactory : IService
{
    void CreateMainMenu();
    void CreateHud(bool isDesktop, SceneID currentScene, Player player);
}
