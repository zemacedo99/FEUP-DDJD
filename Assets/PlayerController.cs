using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;
    public GameObject pulsePrefab;
    public Transform firePoint;

    private float speedBoostMultiplier = 1f;
    private float attackBoostMultiplier = 2f;
    private float speedBoostDurationRemaining = 0f;
    private float attackBoostDurationRemaining = 0f;

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public GameObject bulletPrefab;
    public float firingRate = 1.0f;
    private float originalFiringRate; 
    public float range = 2;

    [SerializeField] private float bulletForce = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalFiringRate = firingRate;
        UpdateScoreText();

        // Call the PeriodicalShooting function every firingRate seconds, starting after an initial delay of 0 seconds
        InvokeRepeating("PeriodicalShooting", 0f, firingRate);
    }

    private void FixedUpdate() 
    {
        if (speedBoostDurationRemaining > 0f)
        {
            speedBoostDurationRemaining -= Time.deltaTime;
        }
        else
        {
            speedBoostMultiplier = 1f;
        }

        if (attackBoostDurationRemaining > 0f)
            {
                attackBoostDurationRemaining -= Time.deltaTime;
                if (firingRate != originalFiringRate / attackBoostMultiplier)
                {
                    firingRate = originalFiringRate / attackBoostMultiplier;
                    CancelInvoke("PeriodicalShooting");
                    InvokeRepeating("PeriodicalShooting", 0f, firingRate);
                }
            }
        else if (firingRate != originalFiringRate)
        {
            firingRate = originalFiringRate;
            CancelInvoke("PeriodicalShooting");
            InvokeRepeating("PeriodicalShooting", 0f, firingRate);
        }

        // If movement input is not 0, try to move
        if(movementInput != Vector2.zero){

            if(movementInput != Vector2.zero){
                
                bool success = TryMove(movementInput);

                if(!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                
                animator.SetBool("isMoving", success);
            } 
         

            // Set direction of sprite to movement direction
            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }

        }
        else {
            animator.SetBool("isMoving", false);
        }


    }

    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime * speedBoostMultiplier);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
        
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    public void ApplySpeedBoost(float duration)
    {
        // Increase movement speed for the specified duration
        speedBoostMultiplier = 1.5f;
        speedBoostDurationRemaining = duration;
    }

    public void ApplyAttackBoost(float duration)
    {
        // Increase the firing rate
        firingRate = originalFiringRate / attackBoostMultiplier;

        // Set the duration of the power-up
        attackBoostDurationRemaining = duration;

    }

    public void addWeapon()
    {
        GameObject pulse = Instantiate(pulsePrefab, firePoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Gear"))
        {
            Destroy(collider.gameObject);
            score++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }



 
    void PeriodicalShooting()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length != 0)
        {
            // Find the closest enemy
            Vector2 closestEnemyPosition = GetClosestEnemyPosition(enemies);

            float distance = Vector2.Distance(firePoint.position, closestEnemyPosition);
            
            if(range >= distance)
            {
            // Calculate the direction of the bullet
            Vector2 shootDirection = ((Vector2)closestEnemyPosition - (Vector2)firePoint.position).normalized;

            // Debug.Log(distance);
            // Fire a bullet in the direction of the closest enemy
            Shoot(shootDirection);
            }

        }

    }

    public void Shoot(Vector2 shootDirection)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        // Debug.Log("Shoot direction: " + bulletForce);
        // Debug.Log("velocity " + rb.velocity);
        rb.velocity = shootDirection * bulletForce;
        // Debug.Log("velocity 2" + rb.velocity);
    }
    
    Vector2 GetClosestEnemyPosition(GameObject[] enemies)
    {        
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(firePoint.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        Vector2 closestEnemyPosition = Vector2.zero;
        if (closestEnemy != null)
        {
            closestEnemyPosition = closestEnemy.transform.position;
            // Debug.Log("Closest enemy position: " + closestEnemyPosition);

        }

        // Vector2 shootDirection = ( (Vector2)closestEnemyPosition- (Vector2)transform.position).normalized;

        return closestEnemyPosition; 
    }

}
