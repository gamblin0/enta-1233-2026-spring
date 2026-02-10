/// <summary>
/// Game over screen
/// Allows for quitting or retrying
/// </summary>
public class LevelComplete : MenuBase
{
    public override GameMenus MenuType()
    {
        return GameMenus.LevelCompleteMenu;
    }

    public void ButtonNextLevel()
    {
        LevelMgr.Instance.LevelIncrease();
        SceneMgr.Instance.LoadScene(GameScenes.Gameplay, GameMenus.InGameUI);
    }

    public void ButtonMainMenu()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
