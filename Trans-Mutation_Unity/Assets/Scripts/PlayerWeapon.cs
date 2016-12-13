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
		if (p.IsFacingRight())
			Debug.Log("todo bien");
		if (Input.GetAxisRaw("Fire1") > 0 && Time.time > nextProjectile){
			nextProjectile = Time.time + shootTimer;
			projectile.GetComponent<Bullet>().dir = p.IsFacingRight();
			Instantiate(projectile, transform.position, transform.rotation);
		}
	}
}
