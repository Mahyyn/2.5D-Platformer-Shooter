using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRandomSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public float spawnInterval = 3.0f;
    public float spawnDistance = 10.0f;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnCollectible());
    }

    IEnumerator SpawnCollectible()
    {
        while (true)
        {
            float randomX = Random.Range(playerTransform.position.x, playerTransform.position.x + spawnDistance);
            Vector3 spawnPosition = new Vector3(randomX, playerTransform.position.y, 0);
            GameObject collectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
        // Check if collectible is outside of camera and destroy it if so
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject collectible in collectibles)
        {
            Vector3 collectibleScreenPoint = Camera.main.WorldToScreenPoint(collectible.transform.position);
            if (collectibleScreenPoint.x < 0 || collectibleScreenPoint.x > Screen.width ||
                collectibleScreenPoint.y < 0 || collectibleScreenPoint.y > Screen.height)
            {
                Destroy(collectible);
            }
        }
    }
}
