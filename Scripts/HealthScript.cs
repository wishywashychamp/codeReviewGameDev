using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthScript : MonoBehaviour
{
    public int maxHealth;           // Max amount of health that the player can have
    private Image fillingImage;     // The reference to the image that is set to filled 
    private int currentHealth;      // The current health of the player


    void Start()
    {
        // Referencing to image that is used to fill the HealthBar
        fillingImage = GetComponentInChildren<Image>();

        // Setting the health to of the player to maximum so the game starts with maximum health
        currentHealth = maxHealth;

        // Calling below function to update the graphics of HealthBar
        UpdateHealthBar();
    }


    // Function to apply damage to the player
    public bool ApplyDamage(int value)
    {
        // Subtracting the specified value coming from the function`s parameter from the current health of the player
        currentHealth -= value;

        // Checking the amount of player`s health, if more than zero (meaning not dead) updating the HealthBar
        if (currentHealth > 0)
        {
            UpdateHealthBar();
            return false;
        }

        // If player has lost all the health than returning true to specify the game is over
        currentHealth = 0;
        UpdateHealthBar();
        return true;
    }

    // Function for updating the fill of the HealthBar 
    void UpdateHealthBar()
    {
        // Storing the percentage of the current amount of health of the player
        float percentage = currentHealth * 1f / maxHealth;

        // Assigning the percentage to the filling amount variable of the image
        fillingImage.fillAmount = percentage;
    }
}
