using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Following is abstarct class, because user can interact with tower in multiple ways (e.g sellling, upgrading, buying) the abstract class has been introduced to share the common functionality among them, this is called Inheritance in OOP.
public abstract class GeneralTowerTradeScript : MonoBehaviour, IPointerClickHandler
{
    protected static MoneyScript moneyMeter;            // Variable to store the money
    protected static TowerScript currentActiveTower;    // Variable to store the current selected tower by the player

    void Start()
    {
        // Checking if the there is a reference for the above money, if not assigning it
        if (moneyMeter == null)
        {
            moneyMeter = FindObjectOfType<MoneyScript>();
        }
    }
    
    // Following function sets the tower to active meaning user has selected this tower for further action
    public static void setActiveTower(TowerScript tower)
    {
        currentActiveTower = tower;
    }

    // Following is and absrtact function meaning the implementation is specific per child scripts (has to be overridden in child)
    public abstract void OnPointerClick(PointerEventData eventData);
}
