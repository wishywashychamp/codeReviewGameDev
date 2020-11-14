using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Inheriting from parent abstract class to share the same functionality 
public class UpgradeTowerScript: GeneralTowerTradeScript 
{
    // Overrideing the parent`s OnPointerClick function, if this is not specified the code will crash
    public override void OnPointerClick(PointerEventData eventData)
    {
        // Checking if the player can afford to upgrade the tower
        if (currentActiveTower.isUpgradable && currentActiveTower.upgradingCost <= moneyMeter.GetMoneyAmount())
        {
            // The payment is executed and the money is removed from the player
            moneyMeter.ChangeMoney(-currentActiveTower.upgradingCost);
            
            // Upgrading the tower
            currentActiveTower.Upgrade();
        }
    }
}
