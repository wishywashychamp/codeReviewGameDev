using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour
{
    public float speed;             // The movement speed of the Enemy Ships
    public float health;            // The amount of health that this ship possesses
    private Rigidbody2D rb2D;       // Variable to store the rigidbody which will be referenced in start function
    public int damageApplied;       // How many damage this EnemyShip applies 

    private AudioSource audioSource; // Variable to store the explosion sound for this prefab
    private Animator animator;      // Variable to store the animator for handling animations

    private static GameManagerScript gameManager;   // This is static variable because all the enemy ships need to have access to the waypoin specified in the game 
    private WaypointScript currentWaypoint;         // Reference to the current Waypoint
    private const float changeDist = 0.001f;        // Private constant under which a waypoint is considered reached

    private MoneyScript moneyMeter;                 // Variable to store the money

    void Start()
    {
        // If the reference to the Game Manager is missing, the script gets it
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManagerScript>();
        }

        // Checking if the there is a reference for the above money, if not assigning it
        if (moneyMeter == null)
        {
            moneyMeter = FindObjectOfType<MoneyScript>();
        }

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    

        // Getting the reference to the Rigidbody2d
        rb2D = GetComponent<Rigidbody2D>();

        // Gtinget the first waypoint from the Game Manager
        currentWaypoint = gameManager.firstWaypoint;
    }

    void FixedUpdate()
    {
        // If there is no more waypoint left than applying the damage to the player and destroying this EnemyShip 
        if (currentWaypoint == null)
        {
            gameManager.DamagePlayersHealth(damageApplied);
            Destroy(gameObject);
            return;
        }

        // Calculating the distance between the EnemyShip and the waypoint that the it is moving towards
        float dist = Vector2.Distance(transform.position, currentWaypoint.GetPosition());

        // If the waypoint is considered reached because below the threshold of the constant changeDist
        if (dist <= changeDist)
        {
            RotateIntoMoveDirection(currentWaypoint.GetPosition());
            currentWaypoint = currentWaypoint.GetNextWaypoint();
        }
        else
        {
            MoveTowards(currentWaypoint.GetPosition());
        }
    }


    // Below function that based on the speed of the EnemyShip makes it moving towards the destination point, specified as Vector3
    private void MoveTowards(Vector3 destination)
    {
        float step = speed * Time.fixedDeltaTime;
        rb2D.MovePosition(Vector3.MoveTowards(transform.position, destination, step));
    }

    private void RotateIntoMoveDirection(Vector3 currentwaypoint)
    {
        // Checking if the there is a next waypoint
        if(currentWaypoint.GetNextWaypoint()) {
            Vector3 newStartPosition = currentWaypoint.transform.position;
            Vector3 newEndPosition = currentWaypoint.GetNextWaypoint().transform.position;
            Vector3 newDirection = (newEndPosition - newStartPosition);
            //
            float x = newDirection.x;
            float y = newDirection.y;
            float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
            //3
            GameObject sprite = gameObject.transform.gameObject;
            gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        }

        
    }

    // Following function takes as input the damage that EnemyShip received when hit by a projectile (called in OnTriggerEnter2D)
    private void Hit(float damage)
    {
        // Subtract the damage to the health of the EnemyShip
        health -= damage;
        // if Ship has no health left meaning less or equals to zero than destroying the EnemyShip and decrementing its number to defeat
        if (health <= 0)
        {
            animator.SetTrigger("DieTrigger");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            audioSource.Play();
            Destroy(gameObject, 1);
            audioSource.Play();
            gameManager.DecrementEnemyShipNumber();
            moneyMeter.ChangeMoney(1);              // adding one to the players money
        }
    }

    // Following function that detects projectiles
    void OnTriggerEnter2D(Collider2D other)
    {
        // Checking if the collison is happening with the Projectile (Note!!! Projectile tag has been spceified for Projectile Prefab)
        if (other.tag == "Projectile")
        {
            // Calling above Hit function to apply a damage to the ships (enemies)
            Hit(other.GetComponent<ProjectileScript>().damage);
            Destroy(other.gameObject);
        }
    }

}
