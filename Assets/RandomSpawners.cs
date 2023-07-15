using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawners : MonoBehaviour
{
    public GameObject spawnItem;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));
            Instantiate(spawnItem, randomSpawnPosition, Quaternion.identity);
        }
    }
}