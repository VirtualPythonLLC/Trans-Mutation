using UnityEngine;
using System.Collections;

/*
 * PlayerWeapon
 * 
 * Handles the Player weapon functionality, i.e shooting
*/
public class PlayerWeapon : MonoBehaviour {

	public GameObject projectile;
	public float shootTimer = 0.15f;
	float diagonalAngle = 0.65f;

	float nextProjectile;
	// Use this for initialization
	void Awake () {
		nextProjectile = 0f;
	}

	public void Fire(float vInput, float hInput){
		Player p = transform.root.GetComponent<Player>();

		if (Time.time > nextProjectile){
			nextProjectile = Time.time + shootTimer;

			//Direction
			if (vInput > 0)
				if (hInput > 0) //upright
					projectile.GetComponent<Bullet>().dir = new Vector2(diagonalAngle,diagonalAngle);
				else 
					if (hInput < 0) //upleft
						projectile.GetComponent<Bullet>().dir = new Vector2(-diagonalAngle,diagonalAngle);
					else //up
						projectile.GetComponent<Bullet>().dir = new Vector2(0,1);
			else
				if (vInput < 0)
					if (hInput > 0) //downright
						projectile.GetComponent<Bullet>().dir = new Vector2(diagonalAngle,-diagonalAngle);
					else 
						if (hInput < 0) //downleft
							projectile.GetComponent<Bullet>().dir = new Vector2(-diagonalAngle,-diagonalAngle);
						else //down
							projectile.GetComponent<Bullet>().dir = new Vector2(0,-1);
				else
					if (p.IsFacingRight()) //right
						projectile.GetComponent<Bullet>().dir = new Vector2(1,0);
					else //left
						projectile.GetComponent<Bullet>().dir = new Vector2(-1,0);

			Instantiate(projectile, transform.position, transform.rotation);
		}
	}
}
