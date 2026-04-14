using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelSelectUI : MenuBase
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private LevelSelectEntry _entryPrefab;

    

    public override GameMenus MenuType()
    {
        return GameMenus.LevelSelectMenu;
    }

    void Start()
    {
       
        BuildEntries();
    }
   

    public void ButtonBack()
    {
        UIMgr.Instance.HideMenu(GameMenus.LevelSelectMenu);
    }

    public void BuildEntries()
    {
        var levels = LevelMgr.Instance.AllLevelData;
        if (levels.Length == 0) return;

        for (var i = 0; i < levels.Length; i++)
        {
            var entry = Instantiate(_entryPrefab, _contentParent);
            entry.Setup(levels[i].SceneName, i);
        }
    }
}
