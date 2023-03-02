using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private int damage = 5;

    [SerializeField] private EnemyData data;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Swarm();
    }

    private void setEnemyValues() {
        GetComponent<Health>().SetHealth(data.health, data.health);
        damage = data.damage;
        speed = data.speed;
    }

    private void Swarm() {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           if(collider.GetComponent<Health>() != null) {
               collider.GetComponent<Health>().TakeDamage(damage);
       
           }
        }
    }
}
