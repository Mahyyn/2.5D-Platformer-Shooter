using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesetGeneration : MonoBehaviour
{

    // The prefab for the tile that will be generated
    public GameObject[] tilePrefabs;
    // The distance between each tile as it's generated
    public float spawnDistance = 10.0f;
    // The distance from the camera that the tiles will be destroyed
    public float destroyDistance = 20.0f;

    // A reference to the main camera's transform
    private Transform playerTransform;
    // The x position of the last tile that was generated
    private float lastTileX = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the reference to the camera's transform
        playerTransform = Camera.main.transform;
        // Spawn the first tile
        SpawnTile();
    }

    // Update is called once per frame
    private void Update()
    {
        // Calculate the distance to the next tile that needs to be generated
        float distanceToNextTile = lastTileX + spawnDistance - playerTransform.position.x - 18;

        // If the distance to the next tile is less than or equal to 0, spawn a new tile
        if (distanceToNextTile <= 0.0f)
        {
            SpawnTile();
        }

        // Check if there are any tiles that need to be destroyed
        DestroyOffscreenTiles();
    }

    // Function to spawn a new tile
    private void SpawnTile()
    {
        // Set the spawn position for the new tile
        Vector3 spawnPosition = transform.position;
        spawnPosition.x = lastTileX + 18;
        spawnPosition.y -= 3.1f;
        // Update the last tile x position
        lastTileX = spawnPosition.x;


        // Instantiate the new tile, with this object as its parent
        Instantiate(tilePrefabs[Random.Range(0,tilePrefabs.Length)], spawnPosition, Quaternion.identity, transform);
    }

    // Function to destroy tiles that are offscreen
    private void DestroyOffscreenTiles()
    {
        // Loop through each child of this object (each tile)
        foreach (Transform child in transform)
        {
            // If the tile is too far to the left of the camera, destroy it
            if (child.position.x + destroyDistance < playerTransform.position.x)
            {
                Destroy(child.gameObject);
            }
        }
    }
}







