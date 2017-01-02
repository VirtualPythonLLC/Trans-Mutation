using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Switch
 * 
 * Script for switches/triggers (buttons, levers, level exits,  etc) logic
*/
public class Switch : MonoBehaviour {

	public GameObject triggeredObject;
	public bool levelExit;
	public string levelToLoad;

	// Use this for initialization
	void Start () {
		
	}

	public void Press() {
		if (levelExit){
			SceneManager.LoadScene(levelToLoad);
		}
		else{
			if (triggeredObject){
				if (triggeredObject.activeSelf)
					triggeredObject.SetActive(false);
				else
					triggeredObject.SetActive(true);
			}
		}
	}

	public bool IsLevelExit(){
		return levelExit;
	}
		
}
