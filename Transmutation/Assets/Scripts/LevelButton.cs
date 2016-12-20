using UnityEngine;
using System.Collections;

/*
 * LevelButton
 * 
 * Script for switches (buttons, levers, etc) logic
*/
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
		if (Input.GetAxisRaw("Fire1") == 0) //Only can re-press a button if player releases the 'use' input
			pressed = false;
	}

	public void Press() {
		// This is only an open-close door behaviour, more can be added
		if (triggeredObject.activeSelf)
			triggeredObject.SetActive(false);
		else
			triggeredObject.SetActive(true);
	}
}
