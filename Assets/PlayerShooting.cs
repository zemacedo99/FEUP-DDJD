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
        if (Input.GetKey(KeyCode.Space) && Time.time - lastShotTime > firingRate)
        {
            Shoot();
        }
    }

    
    Vector2 GetDirectionClosestEnemy()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");

        Vector2 closestEnemyPosition = enemy.transform.position;

        Vector2 shootDirection = ( (Vector2)closestEnemyPosition- (Vector2)transform.position).normalized;

        return shootDirection; 
    }


    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.velocity = GetDirectionClosestEnemy() * bulletForce;
        lastShotTime = Time.time;
    }
}
