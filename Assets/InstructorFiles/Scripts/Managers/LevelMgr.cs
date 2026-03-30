using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMgr : Singleton<LevelMgr>
{
    [Serializable]
    public class LevelData
    {
        public string SceneName;
        public string LevelName;
        public Sprite LevelIcon;    
    }
    [SerializeField] private LevelData[] _allLevelData;
    public LevelData[] AllLevelData => _allLevelData;

    private int _currentLevelIndex;

    public bool IsLevelLoaded { get; private set; }

    public void SetCurrentLevel(int currentLevelIndex)
    {
        _currentLevelIndex = currentLevelIndex;
    }

    public void LoadCurrentLevel()
    {
        IsLevelLoaded = false;
        StartCoroutine(LoadLevelRoutine());
    }

    public void LevelIncrease()
    {
        _currentLevelIndex++;
    }
    private IEnumerator LoadLevelRoutine()
    {
        string levelName = _allLevelData[_currentLevelIndex].LevelName;

        Debug.Log($"LevelMgr: Loading {levelName} additively");

        var asyncOperation = SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Additive);

        while (asyncOperation is { isDone: false}) yield return null;

        Debug.Log("LevelMgr: Level loaded");

        IsLevelLoaded=true;
    }
}