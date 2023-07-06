using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of item prefabs to spawn
    public Transform spawnArea; // The spawn area (empty GameObject or defined area)

    public int numberOfItems = 3; // Number of items to spawn

    public float minSpawnDelay = 1f; // Minimum delay before spawning an item
    public float maxSpawnDelay = 3f; // Maximum delay before spawning an item


    private void Start()
    {
        StartCoroutine(SpawnItemsWithDelay());
    }

    private IEnumerator SpawnItemsWithDelay()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            // Generate random position within the spawn area
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2),
                Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
            );

            // Instantiate a random item prefab at the random position
            GameObject spawnedItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], randomPosition, Quaternion.identity);

          

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}


