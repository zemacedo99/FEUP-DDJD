using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float firingRate = 5.0f;


    public float bulletForce = 1f;

    void Start()
    {
        // Call the PeriodicalShooting function every firingRate seconds, starting after an initial delay of 0 seconds
        InvokeRepeating("PeriodicalShooting", 0f, firingRate);
    }

    void PeriodicalShooting()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length != 0)
        {
            // Find the closest enemy
            Vector2 closestEnemyPosition = GetClosestEnemyPosition(enemies);
            // Calculate the direction of the bullet
            Vector2 shootDirection = ((Vector2)closestEnemyPosition - (Vector2)transform.position).normalized;

            // Fire a bullet in the direction of the closest enemy
            Shoot(shootDirection);
        }

    }

    void Shoot(Vector2 shootDirection)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.velocity = shootDirection * bulletForce;
    }
    
    Vector2 GetClosestEnemyPosition(GameObject[] enemies)
    {        
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
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
