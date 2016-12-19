using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Enemy : MonoBehaviour {

	public float health;
	public float damage;
	public float gravity;
	public float moveSpeed = 1f;
	public float minJumpVelocity = 1f;
	public float maxJumpVelocity = 2f;
	public float movementRangeX;
	public float movementRangeY;

	Vector3 velocity;
	float velocityXSmoothing;
	
	Controller2D controller;	
	Vector2 direction;	
	Animator animator;

	bool facingRight = true;
	Vector2 initPos;
	
	public Transform firePoint;

	bool dead;
	float deathTimer = 1f;
	
	void Start() {
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator>();
		direction = Vector2.right;
		initPos = transform.position;
	}

	void Update() {
		CalculateVelocity ();

		if(!dead)
			controller.Move (velocity * Time.deltaTime, direction);
		
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		
		//Animator
		//animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
		//animator.SetBool("grounded", controller.collisions.below);
		
		//Direction
		if (!controller.collidesWithBullet() && !controller.collidesWithPlayer() && 
			(controller.collisions.left || controller.collisions.right) ||
			(movementRangeX != 0 && (transform.position.x >= initPos.x + movementRangeX || transform.position.x <= initPos.x - movementRangeX))){
			Flip();
			direction *= -1;
		}

		//Damage
		if (controller.collidesWithBullet() && controller.getHitObject()) {
			Bullet b = controller.getHitObject().GetComponent<Bullet>();
			if (b)
				TakeDamage(b.GetDamage());
			//Knockback
			//controller.Move (velocity * Time.deltaTime*3, new Vector2(-1,1));
		}

		//Death
		if (health <= 0){
			//animator.SetBool("dead", true);
			dead = true;
			Destroy(gameObject, deathTimer);
		}
	}

	public void OnJumpInputDown() {
		if (controller.collisions.below) {
			velocity.y = maxJumpVelocity;
		}
	}
	
	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	
	void CalculateVelocity() {
		velocity.x = direction.x * moveSpeed;
		velocity.y += gravity * Time.deltaTime;
	}
	
	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public bool IsFacingRight(){
		return facingRight;
	}
	
	public Controller2D GetController(){
		return controller;
	}

	public float GetDamage(){
		return damage;
	}

	public void TakeDamage(float d){
		health -= d;
	}
}
