using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Inheriting from parent abstract class to share the same functionality 
public class BuyTowerScript : GeneralTowerTradeScript
{
    public GameObject towerPrefab;      // Variable to store which tower is

    // Overrideing the parent`s OnPointerClick function, if this is not specified the code will crash
    public override void OnPointerClick(PointerEventData eventData)
    {
        // Getting the initial cost of the tower
        int price = towerPrefab.GetComponent<TowerScript>().initialCost;

        // Checkinf if the player has enough money to buy the tower
        if (price <= moneyMeter.GetMoneyAmount())
        {

            // Instantiating new Tower is because untill this point the transcaction was successful
            GameObject newTower = Instantiate(towerPrefab);

            // Subtracting the required cost from the user for buying this tower
            moneyMeter.ChangeMoney(-price);

            // Tower is now assigned as the current one (selected one)
            currentActiveTower = newTower.GetComponent<TowerScript>();
        }
    }

}
