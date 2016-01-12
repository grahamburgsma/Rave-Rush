using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/**
    Handles all interaction with the menu
*/
public class MenuHandler : MonoBehaviour {
   
	int scene = 1;

	void Start() {
        
	}

	public void onQuitGame() {
		Application.Quit();
	}

	public void onSceneSelected(int scene) {
		this.scene = scene;
	}

	public void onTimeClick(int time) {
		PlayerPrefs.SetInt("GameLength", time);
		SceneManager.LoadScene(scene);
	}
}
