using UnityEngine;

public interface IUIFactory : IService
{
    void CreateMainMenu();
    void CreateWinWindow(LevelData levelData);
    void CreateFailWindow(LevelData levelData, Player player);
    Hud CreateHud(bool isDesktop, LevelData levelData, Player player);
    ItemView CreateUIKey(Color color, Transform parent);
}
