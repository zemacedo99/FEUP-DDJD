using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour {

    private Transform firePoint;
    private GameObject player;
    private float range;
    public float rangeMax;
    public float rangeSpeed;

    private List<Collider2D> alreadyPingedColliderList;




    void Start() { 
        // Find the prefab in the scene
        player = GameObject.Find("Player");

        // Find the FirePoint object inside the prefab
        firePoint = player.transform.Find("FirePoint");
        transform.position = firePoint.position;

        rangeMax = 0.25f;
        rangeSpeed = 0.2f;
        
        alreadyPingedColliderList = new List<Collider2D>();
    }

    void Update() {
        range += rangeSpeed * Time.deltaTime;
        if (range > rangeMax) {
            range = 0f;
            alreadyPingedColliderList.Clear();
        }
        
        firePoint = player.transform.Find("FirePoint");
        transform.position = firePoint.position;
        transform.localScale = new Vector3(range, range);

   
        RaycastHit2D[] raycastHit2DArray = Physics2D.CircleCastAll(firePoint.position, range, Vector2.zero);
        foreach (RaycastHit2D raycastHit2D in raycastHit2DArray)
        {
            if (raycastHit2D.collider != null) {
                // Hit something
                if (!alreadyPingedColliderList.Contains(raycastHit2D.collider)) {
                    alreadyPingedColliderList.Add(raycastHit2D.collider);
                    
                    if (raycastHit2D.collider.CompareTag("Enemy")) 
                    {
                        Debug.Log("Hit an Enemy");
                        Destroy(raycastHit2D.collider.gameObject);
                    }

                }
            }
        }
    }

}
