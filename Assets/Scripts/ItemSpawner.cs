using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of item prefabs to spawn
    public Transform[] spawnArea; // The spawn area (empty GameObject or defined area)

    public int numberOfItems = 3; // Number of items to spawn

    public float minSpawnDelay = 30f; // Minimum delay before spawning an item
    public float maxSpawnDelay = 60f; // Maximum delay before spawning an item

    public float itemLifetime = 20f; // Duration after which the spawned items will be destroyed

    public float startDelay = 10f; // Delay before starting the spawning process

    private List<Transform> spawnPositions = new List<Transform>(); // List to store available spawn positions
    private List<GameObject> spawnedItems = new List<GameObject>(); // List to store spawned items

    private void Start()
    {
        InitializeSpawnPositions();
        StartCoroutine(StartSpawnItemsWithDelay());
    }

    private void InitializeSpawnPositions()
    {
        // Add available spawn positions to the list
        foreach (Transform spawnPoint in spawnArea)
        {
            spawnPositions.Add(spawnPoint);
        }
    }

    private IEnumerator StartSpawnItemsWithDelay()
    {
        yield return new WaitForSeconds(startDelay); // Delay before starting the spawning process

        StartCoroutine(SpawnItemsWithDelay());
    }

    private IEnumerator SpawnItemsWithDelay()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            // Check if there are available spawn positions
            if (spawnPositions.Count == 0)
            {
                Debug.LogWarning("No available spawn positions.");
                yield break; // Exit the coroutine if there are no available spawn positions
            }

            // Get a random available spawn position
            int randomIndex = Random.Range(0, spawnPositions.Count);
            Transform spawnPosition = spawnPositions[randomIndex];
            spawnPositions.RemoveAt(randomIndex);

            // Instantiate a random item prefab at the spawn position
            GameObject spawnedItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], spawnPosition.position, Quaternion.identity);
            spawnedItems.Add(spawnedItem);

            // Destroy the spawned item after the specified lifetime
            Destroy(spawnedItem, itemLifetime);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
