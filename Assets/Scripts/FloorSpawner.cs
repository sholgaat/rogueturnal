using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject floorPrefab;
    public Transform player;

    [Header("Settings")]
    public int initialChunks = 3;
    public float spawnThreshold = 30f;
    public float despawnDistance = 50f;


    [Header("Enemy")]
    public GameObject enemyPrefab; // assign the enemy prefab in Inspector

    private List<GameObject> activeChunks = new List<GameObject>();
    private float floorWidth;

    void Start()
    {
        if (floorPrefab == null || player == null)
        {
            Debug.LogError("FloorSpawner: Assign floorPrefab and player in inspector.");
            return;
        }

        SpriteRenderer sr = floorPrefab.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            floorWidth = sr.bounds.size.x;
        else
            floorWidth = 10f;

        // Spawn initial chunks
        Vector3 spawnPos = floorPrefab.transform.position;
        for (int i = 0; i < initialChunks; i++)
        {
            GameObject chunk = Instantiate(floorPrefab, spawnPos, Quaternion.identity);
            activeChunks.Add(chunk);

            // Spawn enemies on this chunk
            SpawnEnemiesOnChunk(chunk);

            spawnPos.x += floorWidth;
        }
    }

    void Update()
    {
        if (activeChunks.Count == 0) return;

        GameObject lastChunk = activeChunks[activeChunks.Count - 1];
        if (player.position.x + spawnThreshold > lastChunk.transform.position.x)
        {
            Vector3 spawnPos = new Vector3(
                lastChunk.transform.position.x + floorWidth,
                lastChunk.transform.position.y,
                lastChunk.transform.position.z
            );
            GameObject newChunk = Instantiate(floorPrefab, spawnPos, Quaternion.identity);
            activeChunks.Add(newChunk);

            // Spawn enemies on the new chunk
            SpawnEnemiesOnChunk(newChunk);
        }

        // Despawn chunks that are far behind the player
        List<GameObject> chunksToRemove = new List<GameObject>();
        foreach (GameObject chunk in activeChunks)
        {
            if (player.position.x - chunk.transform.position.x > despawnDistance)
            {
                chunksToRemove.Add(chunk);
            }
        }

        foreach (GameObject chunk in chunksToRemove)
        {
            activeChunks.Remove(chunk);
            Destroy(chunk);
        }
    }

private void SpawnEnemiesOnChunk(GameObject chunk)
{
    Transform spawnParent = chunk.transform.Find("EnemySpawnPoints");
    if (spawnParent == null) return;

    foreach (Transform point in spawnParent)
    {
        Vector3 spawnPos = point.position;

        // Slight random X offset to prevent overlapping
        spawnPos.x += Random.Range(-0.3f, 0.3f);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        EnemyController ec = enemy.GetComponent<EnemyController>();
        if (ec != null)
        {
            // Set patrol limits relative to spawn position
            float patrolRange = 1f; // adjust how far they walk left/right
            ec.leftLimit = spawnPos.x - patrolRange;
            ec.rightLimit = spawnPos.x + patrolRange;
        }
    }
}


}
