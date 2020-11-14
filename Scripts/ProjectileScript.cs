using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float damage;                // The damage this projectile is going to do (to EnemyShip)
    public float speed = 1f;            // The speed of the projectile

    public Vector3 direction;           // The direction the projecyile is going to

    // How long the projectile lives before self-destructing because if the projectile misses the target, it shouldn`t go straight on forever and consume computational resource
    public float lifeDuration = 10f;    

    private Rigidbody2D rb2D;           // Variable to store the rigidbody which will be referenced in start function


    void Start()
    {
        // Getting  the reference to the above created rb2d variable
        rb2D = GetComponent<Rigidbody2D>();     
        
        // Normalizing to keep the same direction with the length of 1.0
        direction = direction.normalized;

        // Rotating the projectile towards the righ direction, so to the EnemyShip
        float angle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // If the projectile misses it has to be destroyed to save computation
        Destroy(gameObject, lifeDuration);
    }


    // Update the position of the projectile according to time and speed
    void FixedUpdate()
    {
        // Just moving the projectile into the direction stored in direction variable of type Vector 3
        rb2D.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
    }


}