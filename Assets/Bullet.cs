using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Trigger entered!");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy destroyed!");

            if(other.gameObject.tag == "Enemy") {
                other.GetComponent<Health>().TakeDamage(50);
                Destroy(gameObject);
            }
           
        }
    }

}
