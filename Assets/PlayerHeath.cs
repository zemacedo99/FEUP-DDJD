using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
    public float healthAmount = 0.255f;

	Vector3 localScale;

	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		localScale.x = healthAmount;
		transform.localScale = localScale;
	}
}
