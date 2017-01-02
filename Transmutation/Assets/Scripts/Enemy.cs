using UnityEngine;
using System.Collections;

/*
 * Enemy
 * 
 * Main script for enemies, handles stats and physic attributes
*/
[RequireComponent (typeof (Controller2D))]
public class Enemy : MonoBehaviour {

	//Stats
	public float health;
	public float damage;

	// Physics
	public float gravity;
	public float moveSpeed = 1f;
	public float minJumpVelocity = 1f;
	public float maxJumpVelocity = 2f;
	Vector3 velocity;
	float velocityXSmoothing;

	//IA
	Vector2 initPos;
	Vector2 destPos;
	float epsilon = 0.1f;
	public float movementRangeX;
	public float movementRangeY;

	//Controller
	Controller2D controller;	
	Vector2 direction;
	bool facingRight = true;

	// Animator
	Animator animator;

	// Other Components
	public Transform firePoint;
	SpriteRenderer sprite;
	Color color;

	// Utils
	bool dead;
	float deathTimer = 1f;

	
	void Start() {
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator>();
		direction = Vector2.right;
		initPos = transform.position;
		sprite = GetComponent<SpriteRenderer>();
		color = sprite.color;
	}
		
	void Update() {
		CalculateVelocity ();

		if(!dead)
			controller.Move (velocity * Time.deltaTime, direction);

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		// Direction
		if (!controller.collidesWithBullet() && !controller.collidesWithPlayer() && 
			(controller.collisions.left || controller.collisions.right) || (movementRangeX != 0 && 
				(facingRight && transform.position.x > initPos.x + movementRangeX || !facingRight && transform.position.x < initPos.x - movementRangeX))){
			Flip();
			direction *= -1;
		}

		//Sprite state
		sprite.color = color;

		// Animator
		//animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
		//animator.SetBool("grounded", controller.collisions.below);

		// Damage
		if (controller.collidesWithPlayer() && controller.getHitObject()) {
			Player p = controller.getHitObject().GetComponent<Player>();
			if (p){
				p.TakeDamage(damage);
			}
		}

		// Death
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
		if (d > 0){
			if (Player.rageOn){ //if player is enraged, damage is increased by 15%
				d += d*0.15f;
			}
			health -= d;
			sprite.color = Color.white;
			Player.rageMeter += 1;
		}
	}

	public bool IsDead(){
		return dead;
	}
}
