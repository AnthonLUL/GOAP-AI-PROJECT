using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShroomManager : MonoBehaviour
{
    public GameObject shroomPrefab;
    public Transform[] shroomSpawnPoints;
    public float respawnTime = 5f;

    private List<GameObject> activeShrooms = new List<GameObject>();

    private void Start()
    {
        // Spawn initial shrooms
        for (int i = 0; i < shroomSpawnPoints.Length; i++)
        {
            SpawnShroom(shroomSpawnPoints[i]);
        }
    }

    private void SpawnShroom(Transform spawnPoint)
    {
        // Instantiate the shroom prefab at the spawn point
        GameObject shroom = Instantiate(shroomPrefab, spawnPoint.position, Quaternion.identity);
        activeShrooms.Add(shroom);
    }

    public void ClearShroom(GameObject shroom)
    {
        // Remove the cleared shroom from the active shrooms list
        activeShrooms.Remove(shroom);

        // Destroy the shroom object
        Destroy(shroom);

        // Delayed respawn
        StartCoroutine(RespawnShroom());
        UnityEngine.Debug.Log("Shroom cleared and respawn initiated.");
    }

    private IEnumerator RespawnShroom()
    {
        yield return new WaitForSeconds(respawnTime);

        // Respawn a shroom after the delay
        if (activeShrooms.Count < shroomSpawnPoints.Length)
        {
            int spawnIndex = UnityEngine.Random.Range(0, shroomSpawnPoints.Length);
            Transform spawnPoint = shroomSpawnPoints[spawnIndex];
            SpawnShroom(spawnPoint);
            UnityEngine.Debug.Log("Shroom respawned.");
        }
    }
}
