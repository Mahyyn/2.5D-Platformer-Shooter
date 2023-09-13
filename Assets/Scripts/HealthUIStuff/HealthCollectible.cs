using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float HealthAdder; // amount of health to add to player when collected

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is the player
        if (collision.tag == "Player")
        {
            // Add health to the player's health in the game.
            collision.GetComponent<Health>().AddHealth(HealthAdder);
            // Disable the health collectible game object
            gameObject.SetActive(false);
        }
    }
}
