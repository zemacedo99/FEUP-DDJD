using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private int damage = 5;

    [SerializeField] private EnemyData data;

    [SerializeField] private NavMeshAgent agent;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Swarm();
        agent.SetDestination(player.transform.position);
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           if(collision.gameObject.GetComponent<Health>() != null) {
               collision.gameObject.GetComponent<Health>().TakeDamage(damage);
       
           }
        }
    }
}
