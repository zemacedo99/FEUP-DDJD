using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    Collider2D collectibleCollider;
    public PlayerHeath healthBar;

    // Start is called before the first frame update
    void Start()
    {
        collectibleCollider = GetComponent<Collider2D>();
        healthBar = FindObjectOfType<PlayerHeath>();
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
                    healthBar.healthAmount += 0.5f;
                    break;
                case "MovSpeed":
                    //give Player movement speet for a limit time
                    break;
                default:
                    // handle any other tag
                    break;
            }
        }

            
    }
}
