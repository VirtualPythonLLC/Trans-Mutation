using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float range = 10f;
	public float damage = 5f;
	public bool dir;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	LineRenderer gunLine;
	
	// Use this for initialization
	void Start () {
	
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
	}
	

	// Update is called once per frame
	void Update () {
		if (dir){
			transform.position = new Vector3(transform.position.x + 0.04f,transform.position.y, transform.position.z);
		}
		else{
			transform.position = new Vector3(transform.position.x - 0.04f,transform.position.y, transform.position.z);
		}
	}
	
}
