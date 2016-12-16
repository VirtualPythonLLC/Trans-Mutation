using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ButtonController))]
public class LevelButton : MonoBehaviour {

	ButtonController controller;
	public GameObject triggeredObject;
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
		if (triggeredObject.layer == LayerMask.NameToLayer("Ground"))
			triggeredObject.layer = LayerMask.NameToLayer("Ignore");
		else
			triggeredObject.layer = LayerMask.NameToLayer("Ground");
		/*
		if (triggeredObject.activeInHierarchy)
			triggeredObject.SetActive(false);
		else
			triggeredObject.SetActive(true);
		*/
	}
}
