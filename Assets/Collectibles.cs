using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    Collider2D collectibleCollider;
    PlayerController player;
    GameObject playerGO;

    // Start is called before the first frame update
    void Start()
    {
        collectibleCollider = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerController>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
                
            switch (collectibleCollider.tag)
            {
                case "Heal":
                    playerGO.GetComponent<Health>().quickHeal(50);
                    Debug.Log("Healed");
                    break;
                case "MovSpeed":
                    player.ApplySpeedBoost(5);
                    break;
                case "PulseWeapon":
                    player.addWeapon();
                    break;
                default:
                    // handle any other tag
                    break;
            }
        }

            
    }
}
