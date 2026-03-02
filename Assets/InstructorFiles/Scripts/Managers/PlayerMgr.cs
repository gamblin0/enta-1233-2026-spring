using JetBrains.Annotations;
using System;
using UnityEngine;

/// <summary>
/// A singleton for communicating with the player object when it exists
/// </summary>
public class PlayerMgr : Singleton<PlayerMgr>
{
    [SerializeField] private GameObject _playerPrefab;
    public GameObject PlayerObject { get; private set; }    
    public bool HasSpawnedPlayer => PlayerObject != null;

    public void SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        if (PlayerObject)
        {
            Debug.LogError("Player already spawnned!");
            return;
        }

        PlayerObject = Instantiate(_playerPrefab, position, rotation);
        Debug.Log("Player spwned");

        
    }
    public void PauseInput()
    {
        GameMgr.Instance.PauseGameToggle();
    }

    public void DebugAssignAsPlayer(GameObject existingPlayer)
    {
        PlayerObject = existingPlayer;
    }
}