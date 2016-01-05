using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour {
	public int goalsScored;


	void Start() {
		goalsScored = 0;
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Ball") {
			goalsScored++;  
		}
	}
}