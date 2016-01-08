using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {
   
	void Start() {
        
	}

	public void onQuitGame() {
		Application.Quit();
	}

	public void onTimeClick(int time) {
		PlayerPrefs.SetInt("GameLength", time);
		SceneManager.LoadScene(1);
	}
}
