using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fading : MonoBehaviour {
	
	public static Image fadeImage;
	private static Animator anim;
	private static string sceneToLoad;
	public static bool loaded = false; // se usa en el optimizador para saber cuando se termino de cargar un nibvel
	public GameObject gameCtrlObj;
	public GameObject youDieGo;
	private static GameObject youDieStatic;
	//private ChunkFactory currentChunkFactory;
	
	
	void Awake(){
		youDieStatic = youDieGo;
		anim = GetComponent<Animator> ();
		fadeImage = GetComponent<Image> ();
	}
	
	public static void BeginFadeIn (string newScene)
	{	loaded = false;
		Player.stopPlayer = true;
		sceneToLoad = newScene;
		anim.SetBool ("FadeOut",false);
		anim.SetBool ("FadeIn",true);
	}
	
	public static void BeginFadeOut ()
	{
		if (sceneToLoad == "level1") { //hay una pantalla negra al principio que se desactiva al cargar el primer nivel
			GameObject bs = GameObject.Find ("blackScreen"); //es para que no se vea nada hasta que se genere todo el lvl
			if(bs != null)
				bs.SetActive(false); 
		}
		SetActiveYouDie(false);
		Player.stopPlayer = false;
		
		//CameraController.stopFollow = false;
		loaded = true;
		anim.SetBool ("FadeOut",true);
		anim.SetBool ("FadeIn",false);
	}
	
	public static void SetActiveYouDie(bool enable){
		youDieStatic.SetActive(enable);
		
	}
	
	public void LoadScene(){
		Application.LoadLevel (sceneToLoad);
	}
	
	public void LevelLoaded(){
		Fading.BeginFadeOut ();
		
	}
	
	void OnLevelWasLoaded (int level) {
		//Debug.Log ("level cargado;: " + level);
	}
	
}