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
            // Debug.Log("Enemy destroyed!");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
