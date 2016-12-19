using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Enemy : MonoBehaviour {

	public int health;
	public int damage;
	public float gravity;
	public float moveSpeed = 1f;
	public float minJumpVelocity = 1f;
	public float maxJumpVelocity = 2f;

	Vector3 velocity;
	float velocityXSmoothing;
	
	Controller2D controller;	
	Vector2 direction;	
	Animator animator;
	
	bool facingRight = true;
	
	public Transform firePoint;
	
	void Start() {
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator>();
		direction = Vector2.right;
	}
	
	void Update() {
		CalculateVelocity ();

		controller.Move (velocity * Time.deltaTime, direction);
		
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		
		//Animator
		//animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
		//animator.SetBool("grounded", controller.collisions.below);
		
		//Direction
		if (!controller.collidesWithPlayer() && (controller.collisions.left || controller.collisions.right)){
			Flip();
			direction *= -1; 
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

	public int GetDamage(){
		return damage;
	}
}
