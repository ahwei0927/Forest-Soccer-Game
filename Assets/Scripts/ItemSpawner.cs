using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnArea;
    public int numberOfItems = 3;
    public float minSpawnDelay = 30f;
    public float maxSpawnDelay = 60f;
    public float itemLifetime = 20f;
    public float startDelay = 10f;

    private List<Transform> spawnPositions = new List<Transform>();
    private List<GameObject> spawnedItems = new List<GameObject>();

    public AudioSource audioS;
    public AudioClip spawnSound;

    private void Start()
    {
        InitializeSpawnPositions();
        StartCoroutine(StartSpawnItemsWithDelay());
    }

    private void InitializeSpawnPositions()
    {
        foreach (Transform spawnPoint in spawnArea)
        {
            spawnPositions.Add(spawnPoint);
        }
    }

    private IEnumerator StartSpawnItemsWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(SpawnItemsWithDelay());
    }

    private IEnumerator SpawnItemsWithDelay()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            if (spawnPositions.Count == 0)
            {
                Debug.LogWarning("No available spawn positions.");
                yield break;
            }

            int randomIndex = Random.Range(0, spawnPositions.Count);
            Transform spawnPosition = spawnPositions[randomIndex];
            spawnPositions.RemoveAt(randomIndex);

            GameObject spawnedItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], spawnPosition.position, Quaternion.identity);
            spawnedItems.Add(spawnedItem);
            audioS.PlayOneShot(spawnSound);

            Destroy(spawnedItem, itemLifetime);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
