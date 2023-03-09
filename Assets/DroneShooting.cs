using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float firingRate = 0.3f;
    public float range = 0.5f;
    
    [SerializeField] private float bulletForce = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        // Call the PeriodicalShooting function every firingRate seconds, starting after an initial delay of 0 seconds
        InvokeRepeating("PeriodicalShooting", 0f, firingRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void PeriodicalShooting()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");


        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
        
        if(range >= distance)
        {
            Vector2 shootDirection = ((Vector2)player.transform.position - (Vector2)gameObject.transform.position).normalized;
            Shoot(shootDirection);
        }

    

    }

    public void Shoot(Vector2 shootDirection)
    {
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        // Debug.Log("Shoot direction: " + bulletForce);
        // Debug.Log("velocity " + rb.velocity);
        rb.velocity = shootDirection * bulletForce;
        // Debug.Log("velocity 2" + rb.velocity);
    }

}
