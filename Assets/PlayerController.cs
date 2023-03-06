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
    private float attackBoostMultiplier = 1f;
    private float boostDurationRemaining = 0f;

    public TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        score = 0;
        UpdateScoreText();
    }

    private void FixedUpdate() 
    {
        if (boostDurationRemaining > 0f)
        {
            // Apply boost
            boostDurationRemaining -= Time.deltaTime;
        }
        else
        {
            // Reset speed boost
            speedBoostMultiplier = 1f;
            attackBoostMultiplier = 1f;
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
        boostDurationRemaining = duration;
    }

    public void ApplyAttackBoost(float duration)
    {
        // Increase movement speed for the specified duration
        attackBoostMultiplier = 1.5f;
        boostDurationRemaining = duration;
    }

    public void HealAmount(float healAmount)
    {
        
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
}
