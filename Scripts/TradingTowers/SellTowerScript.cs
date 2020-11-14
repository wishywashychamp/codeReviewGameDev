using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Inheriting from parent abstract class to share the same functionality 
public class SellTowerScript: GeneralTowerTradeScript
{
    
    // Overrideing the parent`s OnPointerClick function, if this is not specified the code will crash
    public override void OnPointerClick(PointerEventData eventData)
    {
        // Checking if there is a tower selected before to proceed
        if (currentActiveTower == null)
            return;
        // Add towers sell value to the players current owned money
        moneyMeter.ChangeMoney(currentActiveTower.sellingValue);

        // Destroying the Tower from the scene
        Destroy(currentActiveTower.gameObject);

    }
}
