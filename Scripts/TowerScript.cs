using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    //*********************   Variables for Tower transactioninfo *******************//
    public int initialCost;             // The cost of the tower
    public int upgradingCost;           // The cost for upgrading the tower
    public int sellingValue;            // The returned money if the tower is soldd


    //*********************   Variables for Upgrading the Tower *******************//
    public bool isUpgradable = true;    // Checking if tower can be upgraded
    private int upgradeLevel;           // Level of the Cupcake Tower
    public Sprite[] upgradeSprites;     // Array of Sprites that are needed for the upgraded version of Towers


    //*********************   Variables for Tower`s Shooting *******************//
    public float rangeRadius;           // Distance that the Tower is able to shoot at (Cover Radius)
    public float reloadTime;            // Reloading timee nedded for tower to shoot again
    public GameObject projectilePrefab; // Projectile type fired from the Tower
    private float elapsedTime;          // How much time passed since the last shot

    
    void Update()
    {
        // Checking If Tower is able to shoot again by comparing time passed (elapsedTime) with reload time
        if (elapsedTime >= reloadTime)
        {
            // Reseting the time passed so the Tower can shoot again
            elapsedTime = 0;

            // Finding all the gameObjects within the radius(rangeRadius) that contains collider and storing them into Array
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, rangeRadius);

            // Checking if any object exists within the radius 
            if (hitColliders.Length != 0)
            {
                float min = int.MaxValue;
                int index = -1;

                // Looping through the all found object within the range to decide the closest one to the tower
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    // Checking if the Object found is of tag Enemy
                    if (hitColliders[i].tag == "Enemy")
                    {
                        float distance = Vector2.Distance(hitColliders[i].transform.position, transform.position);
                        if (distance < min)
                        {
                            index = i;
                            min = distance;
                        }
                    }
                }
                if (index == -1)
                    return;

                // If the target is found, getting the direction to throw the projectile towards that position
                Transform target = hitColliders[index].transform;
                Vector2 direction = (target.position - transform.position).normalized;

                // Instantiating the stored Projectile toward above stored direction
                GameObject projectile = GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<ProjectileScript>().direction = direction;
            }
        }
        elapsedTime += Time.deltaTime;
    }

    public void Upgrade()
    {
        // Check if the tower can be upgraded
        if (!isUpgradable)
        {
            return;
        }

        // Because it is upgradable at this point incrementing the upgradeLevel variable
        upgradeLevel++;

        // If the tower has reached its maximum level than setting the isUpgradable boolean false
        if (upgradeLevel == upgradeSprites.Length - 1)
        {
            isUpgradable = false;
        }

        // Increase the stats of the tower
        rangeRadius += 1f;
        reloadTime -= 0.1f;

        // Changing the graphics of the tower to the next level
        GetComponent<SpriteRenderer>().sprite = upgradeSprites[upgradeLevel];

        // Increasing the value of the tower beacuse it has been upgraded at this point
        sellingValue += 5;

        // Increasing the upgrading cost beacuse it has been upgraded at this point
        upgradingCost += 10;
    }



    //Function called when the player clicks on the Tower
    void OnMouseDown()
    {
        //Assign this tower as the active tower for trading operations
        GeneralTowerTradeScript.setActiveTower(this);
    }

}
    