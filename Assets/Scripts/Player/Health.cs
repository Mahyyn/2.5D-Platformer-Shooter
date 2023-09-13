using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    // SerializeField attribute is used to show the initial health in the Unity Editor.
    [SerializeField] private float initialHealth;
    [SerializeField] GameOver gameover;
    // Allows us to get and set the current health of the player.
    public float currentHealth { get; private set; }

    // Refers to the Animator component of the player.
    private Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        // Set the current health to the initial health value
        currentHealth = initialHealth;

        // Get the reference to the Animator component of the game object
        anim = GetComponent<Animator>();
    }

    // Function to reduce the current health by the damage taken
    public void DamageTaken(float _damage)
    {
        // Update the current health by subtracting the damage taken
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, initialHealth);

        // Check if the current health is equal to or less than zero
        if (currentHealth <= 0)
        {
            // If so, trigger the "Dead" animation
            anim.SetTrigger("Dead");

            // Disable the PlayerMovement script component
            GetComponent<PlayerMovement>().enabled = false;
            gameover.gameObject.SetActive(true);
        }
    }

    // Update function called every frame
    private void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If so, call the TakeDamage function with 25 as the damage taken
            DamageTaken(25);
        }
    }

    // Function to increase the current health by a value
    public void AddHealth(float _value)
    {
        // Update the current health by adding the value
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, initialHealth);
    }







}
