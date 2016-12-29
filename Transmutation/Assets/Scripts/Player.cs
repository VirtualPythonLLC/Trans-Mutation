using UnityEngine;
using System.Collections;

/*
 * Player
 * 
 * Main Player script, has all the stats and physic properties of player
*/
[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	// Jumping
	public float maxJumpHeight = .4f;
	public float minJumpHeight = .1f;
	public float timeToJumpApex = .4f;

	// Acceleration
	float accelerationTimeAirborne = .02f;
	float accelerationTimeGrounded = .01f;

	// Wall Jumping
	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	// Wall Sliding
	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;
	bool wallSliding;
	int wallDirX;

	// Velocity
	public float moveSpeed = 1f;
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector2 velocity;
	float velocityXSmoothing;

	// Controller
	Controller2D controller;
	Vector2 directionalInput;
	bool facingRight = true;

	// Animator
	// Animator animator;

	// Other Components
	public Transform firePoint;
	SpriteRenderer sprite;

	// Stats
	public float health;

	// Utils
	float deathTimer = 5f;
	bool dead;
	bool grounded;
	float invincibleTime = 1.5f;
	float invincibleTimer;


	public static bool stopPlayer;

	void Start() {
		controller = GetComponent<Controller2D> ();
		sprite = GetComponent<SpriteRenderer>();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

		// animator = GetComponent<Animator>();
	}

	void Update() {
		CalculateVelocity ();
		HandleWallSliding ();

		controller.Move (velocity * Time.deltaTime, directionalInput);

		grounded = controller.collisions.below;
		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}

		// Direction
		if (directionalInput.x < 0 && facingRight || directionalInput.x > 0 && !facingRight){
			Flip();
		}

		// Sprite state
		if (Time.time < invincibleTimer - invincibleTime*0.9f)
			sprite.color = Color.red;
		else
			if (Time.time < invincibleTimer)
				sprite.color = Color.gray;
			else
				sprite.color = Color.white;

		// Animator
		// animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
		// animator.SetBool("grounded", controller.collisions.below);

		//Damage
		if (controller.collidesWithEnemy() && controller.getHitObject()) {
			Enemy e = controller.getHitObject().GetComponent<Enemy>();
			if (e && !e.IsDead()){
				TakeDamage(e.GetDamage());
			}
			// Knockback
			// controller.Move (velocity * Time.deltaTime*3, new Vector2(-1,1));
		}

		if (controller.collidesWithBullet() && controller.getHitObject()) {
			Bullet b = controller.getHitObject().GetComponent<Bullet>();
			if (b && b.IsEnemyBullet()){
				TakeDamage(b.GetDamage());
			}
			// Knockback
			// controller.Move (velocity * Time.deltaTime*3, new Vector2(-1,1));
		}


		// Death
		if (health <= 0){
			Debug.Log ("Game over");
			// animator.SetBool("dead", true);
			dead = true;
			Destroy(gameObject, deathTimer);
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}
		

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		if (controller.collisions.below) //disable air control
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
			//velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
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

	public void TakeDamage(float d){
		if (!dead && Time.time > invincibleTimer){
			health -= d;
			invincibleTimer = Time.time + invincibleTime;
		}
	}

	public bool IsDead(){
		return dead;
	}

	public bool IsGrounded(){
		return grounded;
	}

	public Vector2 GetVelocity(){
		return velocity;
	}

	public PlayerWeapon GetPlayerWeapon(){
		return firePoint.GetComponent<PlayerWeapon>();
	}
}
