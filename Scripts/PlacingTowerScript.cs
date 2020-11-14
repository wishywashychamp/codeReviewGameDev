using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingTowerScript : MonoBehaviour
{
    private GameManagerScript gameManager; // Private variable to store the reference to the Game Manager

    void Start()
    {
        // Getting the reference to the Game Manager
        gameManager = FindObjectOfType<GameManagerScript>();
    }


    void Update()
    {

        float x = Input.mousePosition.x;    // Getting the mouse position
        float y = Input.mousePosition.y;    // Getting the mouse position

        // Placing the Tower where the mouse is
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 7));

        // If the player clicks, the second condition checks if the current position is within an area where Towers can be placed (meaning colider component exists in gameManager)
        if (Input.GetMouseButtonDown(0) && gameManager.isPointerOnAllowedArea())
        {
            // Activating the main Tower script (It was deactivated from inspector beacuse the tower should not shoot or behave like ordinary tower while the player is buying and placing it)
            GetComponent<TowerScript>().enabled = true;
            // Placing a collider on the tower so that another Tower cannot be placed
            gameObject.AddComponent<BoxCollider2D>();
            // Removing this script, so to not keeping the Tower on the mouse
            Destroy(this);
        }

    }
}
