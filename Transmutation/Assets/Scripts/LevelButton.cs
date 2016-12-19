using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ButtonController))]
public class LevelButton : MonoBehaviour {

	ButtonController controller;
	public GameObject triggeredObject;
	bool pressed;
	// Use this for initialization
	void Start () {
		controller = GetComponent<ButtonController> ();
	}
	
	// Update is called once per frame
	void Update(){
		if (Input.GetAxisRaw("Fire1") > 0 && !pressed && controller.CanBePressed()){
			Press();
			pressed = true;
		}
		if (Input.GetAxisRaw("Fire1") == 0)
			pressed = false;
	}

	public void Press() {
		if (triggeredObject.activeSelf)
			triggeredObject.SetActive(false);
		else
			triggeredObject.SetActive(true);
	}
}
