using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // Event function
    private void Start()
    {
        StartCoroutine(StartWhenReady());
    }

    // Frequently called
    private IEnumerator StartWhenReady()
    {
        Debug.Log("GameStarter: Requesting level load");
        LevelMgr.Instance.LoadCurrentLevel();

        Debug.Log("GameStarter: Waiting for level to finish loading...");
        yield return new WaitUntil(() => LevelMgr.Instance.IsLevelLoaded);

        Debug.Log("GameStarter: Spawning player");
        PlayerSpawnPoint spawnPoint = PlayerSpawnPoint.Instance;

        if (spawnPoint == null)

            Debug.LogError("GameStarter: No spawn point found!");

        else

            PlayerMgr.Instance.SpawnPlayer(
                spawnPoint.transform.position,
                spawnPoint.transform.rotation);

        Debug.Log("GameStarter: Waiting for player spawn...");
        yield return new WaitUntil(() => PlayerMgr.Instance.HasSpawnedPlayer);

        Debug.Log("Game starting in 3 seconds...");
        yield return new WaitForSeconds(1f);


    }
}