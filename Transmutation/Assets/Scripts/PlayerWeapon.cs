using UnityEngine;
using System.Collections;

/*
 * PlayerWeapon
 * 
 * Handles the Player weapon functionality, i.e shooting
*/
public class PlayerWeapon : MonoBehaviour {

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
		//Debug.DrawRay(transform.position, projectiles[weapon].GetComponent<Bullet>().dir * 0.2f,Color.cyan);
	}

	public void Fire(float vInput, float hInput){
		Player p = transform.root.GetComponent<Player>();
		Bullet proj = projectiles[weapon].GetComponent<Bullet>();

		if (Time.time > nextProjectile){
			nextProjectile = Time.time + shootTimer;

			//Direction
			if (vInput > 0)
				if (hInput > 0) //upright
					proj.dir = new Vector2(diagonalAngle,diagonalAngle);
				else 
					if (hInput < 0) //upleft
						proj.dir = new Vector2(-diagonalAngle,diagonalAngle);
					else //up
						proj.dir = new Vector2(0,1);
			else
				if (vInput < 0)
					if (hInput > 0) //downright
						proj.dir = new Vector2(diagonalAngle,-diagonalAngle);
					else 
						if (hInput < 0) //downleft
							proj.dir = new Vector2(-diagonalAngle,-diagonalAngle);
						else //down
							proj.dir = new Vector2(0,-1);
				else
					if (p.IsFacingRight()) //right
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
