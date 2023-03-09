using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{

    [SerializeField] private int damage = 50;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Trigger entered!");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy destroyed!");

            if(other.gameObject.tag == "Enemy") {
                other.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
            }
           
        }

        if(other.CompareTag("Player")) {
            Debug.Log("Player hit!");
            if(other.gameObject.tag == "Player") {
                other.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        if(other.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }

}
