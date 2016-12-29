using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

	public GameObject[] projectiles;
	public float shootTimer = 0.15f;
	//public bool drawDirection;
	float diagonalAngle = 0.65f;
	int maxWeapons = 3;
	int weapon;
	float nextProjectile;

	// Use this for initialization
	void Awake () {
		weapon = 0;
		nextProjectile = 0f;
	}

	void Update (){
		Fire();
		//Debug.DrawRay(transform.position, projectiles[weapon].GetComponent<Bullet>().dir * 0.2f,Color.cyan);
	}

	public void Fire(){
		if (Time.time > nextProjectile){
			Enemy e = transform.root.GetComponent<Enemy>();
			Bullet proj = projectiles[weapon].GetComponent<Bullet>();
			nextProjectile = Time.time + shootTimer;

			//Direction
			if (e.IsFacingRight()) //right
				proj.dir = new Vector2(1,0);
			else //left
				proj.dir = new Vector2(-1,0);

			Instantiate(proj, transform.position, transform.rotation);
		}
	}

	public void ChangeWeapon(){
		weapon = (weapon + 1) % maxWeapons;
	}
}
