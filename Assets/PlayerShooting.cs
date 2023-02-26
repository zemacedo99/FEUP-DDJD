using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float firingRate = 5f;
    private float lastShotTime = 0f;

    public float bulletForce = 20f;



    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            // Debug.Log("No enemies found.");
            return;
        }
        
        if (Time.time - lastShotTime > firingRate)
        {
            Shoot(enemies);
        }
    }

    void Shoot(GameObject[] enemies)
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.velocity = GetDirectionClosestEnemy(enemies) * bulletForce;
        lastShotTime = Time.time;
    }
    
    Vector2 GetDirectionClosestEnemy(GameObject[] enemies)
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
            Debug.Log("Closest enemy position: " + closestEnemyPosition);

        }

        Vector2 shootDirection = ( (Vector2)closestEnemyPosition- (Vector2)transform.position).normalized;

        return shootDirection; 
    }



}
