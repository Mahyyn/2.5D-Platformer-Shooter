using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{

    // This refers to the player's health component
    [SerializeField] private Health playerHealth;

    // This refers to the total health bar UI image
    [SerializeField] private Image FullHealthBar;

    //This refers to the current health bar UI image
    [SerializeField] private Image UpdatedHealthBar;

    // Start is called before the first frame update
    private void Start()
    {
        // Set the total health bar fill amount to the player's current health divided by 100 because health is 100
        //and max fill for the UI is 1, therefore to base it off of 1 you divide by 100.
        FullHealthBar.fillAmount = playerHealth.currentHealth / 100;
    }

    // Update is called once per frame
    private void Update()
    {
        // Set the current health bar fill amount to the player's current health divided by 100
        UpdatedHealthBar.fillAmount = playerHealth.currentHealth / 100;
    }


}
