using UnityEngine;
using System.Collections;

/*
 * Bullet
 * 
 * Script for PLAYER bullets, handles stats and movement
*/
public class Bullet : MonoBehaviour {

	// Stats
	public float range = 10f;
	public float damage = 5f;
	public float speed = 0.07f;

	// Utils
	public float aliveTime;

	// Controller
	Controller2D controller;
	public Vector2 dir;

	
	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
	}

	void Awake (){
		Vector2 scale = transform.localScale;

		if (dir.x < 0){
			scale.x *= -1;
			if (dir.y < 0){
				transform.rotation = Quaternion.Euler(0,0,45);
			}
			if (dir.y > 0){
				transform.rotation = Quaternion.Euler(0,0,-45);
			}
		}
		if (dir.x > 0){
			if (dir.y < 0){
				transform.rotation = Quaternion.Euler(0,0,-45);
			}
			if (dir.y > 0){
				transform.rotation = Quaternion.Euler(0,0,45);
			}
		}
		transform.localScale = scale;

		Destroy (gameObject, aliveTime);
	}
	

	// Update is called once per frame
	void Update () {
		//Inflict damage
		if (controller.collidesWithEnemy() && controller.getHitObject()) {
			Enemy e = controller.getHitObject().GetComponent<Enemy>();
			if (e && !e.IsDead()){
				e.TakeDamage(damage);
			}
		}
		// Destroy on collision
		if (controller.HasCollisions())
			Destroy (gameObject);
		// Movement
		controller.Move (dir * speed, false);
		//transform.position = new Vector3(transform.position.x + 0.04f * dir.x,transform.position.y + 0.04f * dir.y, transform.position.z);
	}

	public float GetDamage(){
		return damage;
	}
	
}
