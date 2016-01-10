using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombTrigger : MonoBehaviour {

	[SerializeField] Rigidbody car;
	[SerializeField] GameObject ball;
	[SerializeField] GameObject explosion;

	void Start() {
	}

	void Update() {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {

			//Visual bomb explosion
			Transform explosion_transform = explosion.GetComponent<Transform>();
			Transform ball_transform = ball.GetComponent<Transform>();
			explosion_transform.position = ball_transform.position;
			ParticleSystem explosion_particle = explosion.GetComponent<ParticleSystem>();
			explosion_particle.startColor = Color.red;
			explosion_particle.Play();

			//Put ball back to the middle
			ball.transform.position = new Vector3(0, 25, 0);
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}
}