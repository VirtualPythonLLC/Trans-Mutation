using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float range = 10f;
	public float damage = 5f;
	public float speed = 0.07f;
	public Vector2 dir;
	public float aliveTime;

	/*Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	LineRenderer gunLine;*/

	Controller2D controller;

	
	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
	}

	void Awake (){
		/*shootableMask = LayerMask.GetMask("Shootable");
		gunLine = GetComponent<LineRenderer>();

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		gunLine.SetPosition(0, transform.position);

		if(Physics.Raycast(shootRay, out shootHit, range, shootableMask)){
			gunLine.SetPosition(1, shootHit.point);
		} else{
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
		}*/
		Vector3 scale = transform.localScale;

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
		//check collision
		if (controller.HasCollisions())
			Destroy (gameObject);
		//move
		controller.Move (dir * speed, false);
		//transform.position = new Vector3(transform.position.x + 0.04f * dir.x,transform.position.y + 0.04f * dir.y, transform.position.z);
	}

	public float GetDamage(){
		return damage;
	}
	
}
