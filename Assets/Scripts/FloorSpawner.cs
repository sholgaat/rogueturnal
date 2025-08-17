using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject floorPrefab;
    public Transform player;

    [Header("Settings")]
    public int initialChunks = 3;          // how many chunks to spawn initially
    public float spawnThreshold = 30f;     // distance from last chunk to player before spawning next
    public float despawnDistance = 50f;    // distance behind player to destroy old chunks

    private List<GameObject> activeChunks = new List<GameObject>();
    private float floorWidth;

    void Start()
    {
        if (floorPrefab == null || player == null)
        {
            Debug.LogError("FloorSpawner: Assign floorPrefab and player in inspector.");
            return;
        }

        // Assuming the prefab has a SpriteRenderer or Tilemap Renderer to get width
        SpriteRenderer sr = floorPrefab.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            floorWidth = sr.bounds.size.x;
        else
        {
            // Debug.LogWarning("FloorSpawner: Could not find SpriteRenderer in prefab. Using 10 as default width.");
            floorWidth = 10f;
        }

        // Spawn initial chunks
        Vector3 spawnPos = floorPrefab.transform.position;
        for (int i = 0; i < initialChunks; i++)
        {
            GameObject chunk = Instantiate(floorPrefab, spawnPos, Quaternion.identity);
            activeChunks.Add(chunk);
            spawnPos.x += floorWidth;
        }
    }

    void Update()
    {
        if (activeChunks.Count == 0) return;

        // Spawn new chunk if player is close enough to the last chunk
        GameObject lastChunk = activeChunks[activeChunks.Count - 1];
        if (player.position.x + spawnThreshold > lastChunk.transform.position.x)
        {
            Vector3 spawnPos = new Vector3(
                lastChunk.transform.position.x + floorWidth,
                lastChunk.transform.position.y, // keep Y consistent
                lastChunk.transform.position.z
            );
            GameObject newChunk = Instantiate(floorPrefab, spawnPos, Quaternion.identity);
            activeChunks.Add(newChunk);
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
}
