using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{


[SerializeField] 
private GameObject RobotPrefab;

[SerializeField]
private GameObject DronePrefab;

[SerializeField] private float swarmerInterval = 3.5f;
[SerializeField] private float droneInterval = 2.0f;

private Coroutine swarmerCoroutine;
private Coroutine droneCoroutine;


private int score = 0;

private GameObject player ;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swarmerCoroutine =  StartCoroutine(SpawnEnemy(swarmerInterval, RobotPrefab));
        droneCoroutine =  StartCoroutine(SpawnEnemy(droneInterval, DronePrefab));

    }

    void Update()
    {
        score = player.GetComponent<PlayerController>().GetScore();

        if(score > 5){
            DronePrefab.GetComponent<Health>().MAX_HEALTH = 400;
            RobotPrefab.GetComponent<Health>().MAX_HEALTH = 600;
        }

        if(score > 10){
            swarmerInterval = 2.5f;
            droneInterval = 1.0f;
            
        }

        if(score > 20){
            swarmerInterval = 1.5f;
            droneInterval = 0.5f;
            
        }

        if(score > 30){
            swarmerInterval = 0.5f;
            droneInterval = 0.1f;
            
        }

        


    }

    
    private IEnumerator SpawnEnemy(float interval, GameObject enemy) {
        
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f,5), Random.Range(-6f,6), 0), Quaternion.identity);
         StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
