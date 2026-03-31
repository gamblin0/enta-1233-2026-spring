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
        string sceneName = _allLevelData[_currentLevelIndex].SceneName;

        Debug.Log($"LevelMgr: Loading {sceneName} additively");

        var asyncOperation = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

        while (asyncOperation is { isDone: false}) yield return null;

        Debug.Log("LevelMgr: Level loaded");

        IsLevelLoaded=true;
    }
}