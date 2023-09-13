using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Transform[] EnemySpawnPoints;//Creating spawn points for 2 random enemies and health.
    public Transform HealthSpawnPoint;
    public GameObject Enemy; // Gets the Enemy GameObject.
    public GameObject Health; //Gets the Health GameObject.
    void Start()
    {

        foreach (var enemySpawnPoint in EnemySpawnPoints)
        {
            if (Random.Range(0, 2) == 1)
            {
                Instantiate(Enemy, enemySpawnPoint.position, Quaternion.identity);
            }
        }

        //Chooses a random number from 1 to 5 and if it is 1 then the enemies and the health collectible is spawned.
        if (Random.Range(0,5)== 1)
            {
              Instantiate(Health, HealthSpawnPoint.position, Quaternion.identity);
              
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
