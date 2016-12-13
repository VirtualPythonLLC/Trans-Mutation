using UnityEngine;
using System.Collections;
/*#############UNUSED##############*/
public class DestroyObject : MonoBehaviour {

	public float aliveTime;
	public bool destroyOnCollision;

	// Use this for initialization
	void Awake () {
		Destroy (gameObject, aliveTime);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
		//	coll.gameObject.SendMessage("ApplyDamage", 10);
		Destroy (gameObject);
	}
}
