using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
   public GameObject waterPrefab;
    public GameObject checkpointPrefab;
    public GameObject collectiblePrefab;

    public Transform player;
    public float spawnDistance = 50f;
    public float spawnInterval = 20f;
    public int poolSize = 10;

    private Queue<GameObject> waterPool = new Queue<GameObject>();
    private Queue<GameObject> checkpointPool = new Queue<GameObject>();
    private Queue<GameObject> collectiblePool = new Queue<GameObject>();

    private float nextSpawnZ = 0f;

    private void Start()
    {
        // Initialize object pools
        InitializePool(waterPrefab, waterPool, poolSize);
        InitializePool(checkpointPrefab, checkpointPool, poolSize / 2); // Fewer checkpoints
        InitializePool(collectiblePrefab, collectiblePool, poolSize / 2); // Fewer collectibles
    }

    private void Update()
    {
        if (player.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        // Spawn Water
        ReuseObject(waterPool, new Vector3(0, 0, nextSpawnZ));

        // Randomly spawn Checkpoints
        if (Random.value > 0.5f) 
            ReuseObject(checkpointPool, new Vector3(0, 1, nextSpawnZ + 10));

        // Randomly spawn Collectibles
        if (Random.value > 0.3f) 
            ReuseObject(collectiblePool, new Vector3(Random.Range(-5f, 5f), 1, nextSpawnZ + 5));

        nextSpawnZ += spawnInterval;
    }

    private void InitializePool(GameObject prefab, Queue<GameObject> pool, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    private void ReuseObject(Queue<GameObject> pool, Vector3 position)
    {
        GameObject obj = pool.Dequeue();
        obj.transform.position = position;
        obj.SetActive(true);
        pool.Enqueue(obj);
    }
}
