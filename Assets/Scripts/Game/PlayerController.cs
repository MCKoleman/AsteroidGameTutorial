using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement info
    [SerializeField]
    private float moveSpeed;                            // Movement speed of the player
    private Rigidbody2D rb;                             // Rigidbody of the player
    private const float decelerationRate = 5.0f;        // Rate at which the player decelerates

    // Firing info
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private GameObject fireProjectilePrefab;
    [SerializeField]
    private float maxFireCooldown;
    [SerializeField]
    private float curFireCooldown;

    // Energy info
    [SerializeField]
    private float energyRegenMod;
    [SerializeField]
    private float fireCostDecayMod;
    [SerializeField]
    private float baseFireEnergyCost;
    [SerializeField]
    private float curFireEnergyCost;
    [SerializeField]
    private float maxEnergy;
    [SerializeField]
    private float curEnergy;

    private void Start()
    {
        // Gets the rigidbody attached to the same game object as this script
        rb = this.GetComponent<Rigidbody2D>();

        // Stop player from shooting at the very beginning of the game
        curFireCooldown = maxFireCooldown;

        // Set player's energy to max
        curEnergy = maxEnergy;
        curFireEnergyCost = baseFireEnergyCost;
    }

    private void Update()
    {
        // Only run checks when the game is active
        if(GameManager.Instance.IsGameActive)
        {
            HandleMovement();
            HandleFire();
        }
    }

    // Handles player movement
    private void HandleMovement()
    {
        float moveDir = Input.GetAxis("Vertical");

        // When input is pressed, accelerate the player
        if (moveDir != 0)
            rb.velocity = new Vector2(0.0f, moveDir * moveSpeed);
        // When input is not pressed, declerate the player
        else
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * decelerationRate);
    }

    // Handles player firing
    private void HandleFire()
    {
        // If the fire cooldown is over, check for fire inputs
        if (curFireCooldown <= 0.0f)
        {
            // Check if any fire inputs are pressed (Space, Right Control, A/X on controller or Right Trigger)
            if(Input.GetKey(KeyCode.Space) 
                || Input.GetKey(KeyCode.RightControl)
                || Input.GetKey(KeyCode.Joystick1Button0)
                || Input.GetKey(KeyCode.Joystick1Button10))
            {
                // If player has enough energy to fire, fire
                if(curEnergy >= curFireEnergyCost)
                {
                    // Remove cost of firing from player's energy
                    curEnergy -= curFireEnergyCost;

                    // Increase cost of firing for each consecutive shot
                    curFireEnergyCost += baseFireEnergyCost;

                    // Reset the cooldown
                    curFireCooldown = maxFireCooldown;

                    // Fire a projectile
                    GameObject newProjectile = Instantiate(fireProjectilePrefab, firePoint.transform.position, Quaternion.Euler(0.0f, 0.0f, -90.0f));
                    newProjectile.GetComponent<Projectile>().Init();
                    newProjectile.transform.SetParent(SpawnManager.Instance.spawnHolder.transform);
                }
            }
            // Only regen energy when player is not attempting to fire
            else
            {
                // Regen energy
                curEnergy += Time.deltaTime * energyRegenMod;

                // Decrease cost of fire
                curFireEnergyCost = Mathf.Clamp(curFireEnergyCost - Time.deltaTime * fireCostDecayMod, baseFireEnergyCost, maxEnergy);
            }
        }
        // If the timer hasn't finished, count it down
        else
        {
            curFireCooldown -= Time.deltaTime;
        }

        // Updates the player's energy display
        UpdateEnergyDisplay();
    }

    // Updates the energy display in the UI
    private void UpdateEnergyDisplay()
    {

    }

    // Kills the player, prompting respawn and resetting UI
    private void KillPlayer()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        GameManager.Instance.EndGame();
    }

    // Respawns the player
    private void RespawnPlayer()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
    }

    // Event for when the player collides with any other object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an asteroid
        if(collision.collider.CompareTag("Asteroid"))
        {
            KillPlayer();
        }
    }
}
