
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelectEntry : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private TMP_Text _levelNameText;
    [SerializeField] private TMP_Text _partTimeText;
    [SerializeField] private TMP_Text _bestTimeText;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private GameObject _lockedRoot;

    #endregion

    private int _levelIndex;

    private void Awake()
    {
        if (_button == null) _button = GetComponent<Button>();
    }

    public void Setup(string level, int levelIndex)
    {
        _levelIndex = levelIndex;

        if (_titleText != null) _titleText.text = level;
    }

    public void ButtonPressed()
    {
        LevelMgr.Instance.SetCurrentLevel(_levelIndex);
        SceneMgr.Instance.LoadScene(GameScenes.Gameplay, GameMenus.InGameUI);
    }
}
