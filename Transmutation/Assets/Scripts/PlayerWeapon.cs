using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public GameObject projectile;
	public float shootTimer = 0.15f;
	float diagonalAngle = 0.65f;

	float nextProjectile;
	// Use this for initialization
	void Awake () {
		nextProjectile = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Player p = transform.root.GetComponent<Player>();

		if (Input.GetAxisRaw("Fire1") > 0 && Time.time > nextProjectile){
			nextProjectile = Time.time + shootTimer;

			//Direction
			if (Input.GetAxisRaw("Vertical") > 0)
				if (Input.GetAxisRaw("Horizontal") > 0) //upright
					projectile.GetComponent<Bullet>().dir = new Vector2(diagonalAngle,diagonalAngle);
				else 
					if (Input.GetAxisRaw("Horizontal") < 0) //upleft
						projectile.GetComponent<Bullet>().dir = new Vector2(-diagonalAngle,diagonalAngle);
					else //up
						projectile.GetComponent<Bullet>().dir = new Vector2(0,1);
			else
				if (Input.GetAxisRaw("Vertical") < 0)
					if (Input.GetAxisRaw("Horizontal") > 0) //downright
						projectile.GetComponent<Bullet>().dir = new Vector2(diagonalAngle,-diagonalAngle);
					else 
						if (Input.GetAxisRaw("Horizontal") < 0) //downleft
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
