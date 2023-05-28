using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform[] treeSpawnPoints;
    public float respawnTime = 10f;

    private List<GameObject> activeTrees = new List<GameObject>();

    private void Start()
    {
        // Spawn initial trees
        for (int i = 0; i < treeSpawnPoints.Length; i++)
        {
            SpawnTree(treeSpawnPoints[i]);
        }
    }

    private void SpawnTree(Transform spawnPoint)
    {
        // Instantiate the tree prefab at the spawn point
        GameObject tree = Instantiate(treePrefab, spawnPoint.position, Quaternion.identity);
        activeTrees.Add(tree);
    }

    public void ClearTree(GameObject tree)
    {
        // Remove the cleared tree from the active trees list
        activeTrees.Remove(tree);

        // Destroy the tree object
        Destroy(tree);

        // Delayed respawn
        StartCoroutine(RespawnTree());
        UnityEngine.Debug.Log("Tree cleared and respawn initiated.");
    }

    private IEnumerator RespawnTree()
    {
        yield return new WaitForSeconds(respawnTime);

        // Respawn a tree after the delay
        if (activeTrees.Count < treeSpawnPoints.Length)
        {
            int spawnIndex = UnityEngine.Random.Range(0, treeSpawnPoints.Length);
            Transform spawnPoint = treeSpawnPoints[spawnIndex];
            SpawnTree(spawnPoint);
            UnityEngine.Debug.Log("Tree respawned.");
        }
    }
}
