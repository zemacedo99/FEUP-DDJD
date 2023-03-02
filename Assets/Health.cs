using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

	[SerializeField] private int health = 100;

	private int MAX_HEALTH = 200;

	private GameObject healthBar;




	Vector3 localScale;

	// Use this for initialization
	void Start () {


		if(gameObject.tag == "Player") {

			healthBar = GameObject.FindGameObjectWithTag("HealthBar");
			localScale = healthBar.transform.localScale;
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if(gameObject.tag == "Player") {
			localScale.x = health/200f;
			healthBar.transform.localScale = localScale;
		}
		
	}


	public void TakeDamage (int damage) {
		if(damage < 0) {
			throw new System.ArgumentOutOfRangeException("damage", "Damage must be positive");
		}

		if(health - damage <= 0) {
			Die();
		} else {
			health -= damage;
		}
	}

	public void SetHealth(int maxHealth, int health) {
		this.MAX_HEALTH = maxHealth;
		this.health = health;
	}

	public void quickHeal(int heal) {
		if(heal < 0) {
			throw new System.ArgumentOutOfRangeException("heal", "Heal must be positive");
		}

		if(health + heal > MAX_HEALTH) {
			health = MAX_HEALTH;
		} else {
			health += heal;
		}
	}


	private void Die() {
		Debug.Log("I died");
		Destroy(gameObject);
	}


}
