using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public string levelToLoad;
	bool playerInZone;

	// Use this for initialization
	void Start () {
		playerInZone = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) && playerInZone)
			SceneManager.LoadScene(levelToLoad);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player")
			playerInZone = true;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.name == "Player")
			playerInZone = false;
	}
}
