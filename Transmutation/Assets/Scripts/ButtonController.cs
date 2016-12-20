using UnityEngine;
using System.Collections;

/*
 * ButtonController
 * 
 * Controller for switches (buttons, levers, etc), special treatment for collisions
*/

public class ButtonController : RaycastController {
		

	public override void Start() {
		base.Start ();
		UpdateRaycastOrigins ();
	}
	
	public bool CanBePressed() {
		float rayLength = skinWidth;

		if (1 < skinWidth) {
			rayLength = 2*skinWidth;
		}

		for (int i = 0; i < horizontalRayCount; i ++) {
			//check Left collisions
			Vector2 rayOrigin = raycastOrigins.bottomLeft;
			Debug.Log (rayOrigin.ToString());
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.left, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.left * 0.2f,Color.red);
			
			if (hit) {
				if (hit.collider.tag == "Player"){
					return true;
				}
			}
			//check Right collisions
			rayOrigin = raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, collisionMask);
			
			Debug.DrawRay(rayOrigin, Vector2.right * 0.2f,Color.red);
			
			if (hit) {
				if (hit.collider.tag == "Player"){
					return true;
				}
			}
		}

		return false;
	}	
}
