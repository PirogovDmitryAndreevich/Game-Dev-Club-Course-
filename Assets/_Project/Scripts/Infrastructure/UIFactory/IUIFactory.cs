using UnityEngine;

public interface IUIFactory : IService
{
    void CreateMainMenu();
    WinWindow CreateWinWindow(LevelData levelData);
    void CreateFailWindow(LevelData levelData, Player player);
    Hud CreateHud(bool isDesktop, LevelData levelData, Player player);
    ItemView CreateUIKey(Color color, Transform parent);
    LevelCard CreateLevelCard(LevelData data, Transform parent);
}
