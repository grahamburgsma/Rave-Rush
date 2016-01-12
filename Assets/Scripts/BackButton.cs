using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/** 
    Handles the back button from the in-game UI
*/
public class BackButton : MonoBehaviour {

	public void backPressed()
    {
        SceneManager.LoadScene(0);
    }

}
