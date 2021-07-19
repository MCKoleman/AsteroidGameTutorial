using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Asteroid initialization info
    [SerializeField]
    private float MIN_SPEED = 0.1f;
    [SerializeField]
    private float MAX_SPEED = 3.0f;
    [SerializeField]
    private float MIN_SIZE = 0.02f;
    [SerializeField]
    private float MAX_SIZE = 2.0f;
    [SerializeField]
    private float MIN_POS = -5.0f;
    [SerializeField]
    private float MAX_POS = 5.0f;
    [SerializeField]
    private float LEVEL_SPEED_MOD = 0.1f;

    // Asteroid component info
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int curHealth;
    private const int SCORE_MOD = 5;
    private Rigidbody2D rb;

    // Initializes the asteroid
    public void Init(int level = 0)
    {
        rb = this.GetComponent<Rigidbody2D>();

        // Initializes the asteroid to have random attributes
        float levelSpeedMod = Mathf.Max(Mathf.Pow(1.0f + LEVEL_SPEED_MOD, 1.0f + 0.1f * level), 1.0f);
        float randomSpeed = Random.Range(MIN_SPEED, MAX_SPEED) * levelSpeedMod;
        float randomSize = (Random.Range(MIN_SIZE, MAX_SIZE) + Random.Range(MIN_SIZE, MAX_SIZE)) / 2.0f;
        float randomPos = Random.Range(MIN_POS, MAX_POS);
        float randomRotation = Random.Range(0, 360);

        // Sets the random attributes to the components of the asteroid
        rb.velocity = randomSpeed * -Vector2.right;
        this.transform.position = new Vector3(this.transform.position.x, randomPos, 0.0f);
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomRotation);
        this.transform.localScale = new Vector3(randomSize, randomSize, 1.0f);

        // Sets the health of the asteroid
        maxHealth = Mathf.Max(Mathf.RoundToInt(Mathf.Pow(2.0f, 1.0f + 1.5f * randomSize)), 1);
        curHealth = maxHealth;
    }

    // Takes damage for the asteroid.
    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        // If health drops to or below 0, destroy the asteroid
        if(curHealth <= 0)
        {
            // When the asteroid is destroyed, increment the player's score
            // TODO: Increase score
            Destroy(this.gameObject);
        }
    }
}
