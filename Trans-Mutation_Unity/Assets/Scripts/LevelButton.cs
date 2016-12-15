using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ButtonController))]
public class LevelButton : MonoBehaviour {

	ButtonController controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<ButtonController> ();
	}
	
	// Update is called once per frame
	void Update(){
		if (Input.GetAxisRaw("Fire1") > 0 && controller.CanBePressed())
			Press();
	}

	public void Press() {
		Debug.Log("button pressed");
	}
}
