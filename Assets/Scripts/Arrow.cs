using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	[SerializeField] Transform ball;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
		transform.LookAt(ball.position);
	}
}
