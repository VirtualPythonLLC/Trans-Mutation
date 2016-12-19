using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public GameObject projectile;
	public float shootTimer = 0.15f;

	float nextProjectile;
	// Use this for initialization
	void Awake () {
		nextProjectile = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Player p = transform.root.GetComponent<Player>();

		if (Input.GetAxisRaw("Fire1") > 0 && Time.time > nextProjectile){
			nextProjectile = Time.time + shootTimer;
			if (Input.GetAxisRaw("Vertical") > 0){
				if (p.IsFacingRight())
					projectile.GetComponent<Bullet>().dir = new Vector2(0.65f,0.65f);
				else
					projectile.GetComponent<Bullet>().dir = new Vector2(-0.65f,0.65f);
			} else{
				if (Input.GetAxisRaw("Vertical") < 0){
					if (p.IsFacingRight())
						projectile.GetComponent<Bullet>().dir = new Vector2(0.65f,-0.65f);
					else
						projectile.GetComponent<Bullet>().dir = new Vector2(-0.65f,-0.65f);
				}
				else{
					if (p.IsFacingRight())
						projectile.GetComponent<Bullet>().dir = new Vector2(1,0);
					else
						projectile.GetComponent<Bullet>().dir = new Vector2(-1,0);
				}
			} 

			Instantiate(projectile, transform.position, transform.rotation);
		}
	}
}
