using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int damage;

    private Rigidbody2D rb;

    // Initializes the projectile and sets it into motion
    public void Init(float _moveSpeed = -1.0f, int _damage = -1)
    {
        rb = this.GetComponent<Rigidbody2D>();

        // If input was given for the speed, set the speed of the projectile
        if (_moveSpeed != -1.0f)
            moveSpeed = _moveSpeed;

        // If input was given for the damage, set the damage of the projectile
        if (_damage != -1)
            damage = _damage;

        // Initialize the speed of the projectile
        rb.velocity = Vector2.right * moveSpeed;
    }

    // Event for when this object collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore projectile and player collisions
        if(collision.CompareTag("Player") || collision.CompareTag("Projectile"))
        {
            return;
        }
        // If the projectile hit an asteroid, deal damage to it
        else if(collision.CompareTag("Asteroid"))
        {
            // Get the asteroid that was hit
            Asteroid hitAsteroid = collision.gameObject.GetComponent<Asteroid>();

            // If the asteroid exists, deal damage to it
            if (hitAsteroid != null)
                hitAsteroid.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
