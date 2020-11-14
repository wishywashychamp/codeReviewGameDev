using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public int startMoney = 12;
    private Text moneyText;         // Reference to the Text component that displays the money amount
    private int currentMoney;       // Amount of money that the player currently owns


    void Start()
    {
        // Getting the reference to Money Text UI inside the children
        moneyText = GetComponentInChildren<Text>();
        currentMoney = startMoney; // initializing the curent Money to start with 10

        // Update the Money bar at the top of the UI 
        UpdateMoneyTextUI();
    }
    

    // Function that increases or decreases the current money owned by the user
    public void ChangeMoney(int value)
    {
        // Increasing or decresing the value (increasing because there will be an option to sell towers)
        currentMoney += value;

        // The money cann`t be negative in this game, so if it becomes negative setting it to zero
        if (currentMoney < 0)
        {
            currentMoney = 0;
        }

        // Calling function to update the Text UI
        UpdateMoneyTextUI();
    }

    // Function that return the current money owned by the player
    public int GetMoneyAmount()
    {
        return currentMoney;
    }

    // Following function updates the money text UI by converting the money owned by use to a string beacuse the money is displayed in Text inside the Games TopBar
    void UpdateMoneyTextUI()
    {
        moneyText.text = currentMoney.ToString();
    }


}
