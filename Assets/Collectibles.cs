using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    Collider2D collectibleCollider;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        collectibleCollider = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
                
            switch (collectibleCollider.tag)
            {
                case "Heal":
                    // player.healthBar.healthAmount += 0.5f;
                    player.HealAmount(0.5f);
                    break;
                case "MovSpeed":
                    player.ApplySpeedBoost(5);
                    break;
                default:
                    // handle any other tag
                    break;
            }
        }

            
    }
}
