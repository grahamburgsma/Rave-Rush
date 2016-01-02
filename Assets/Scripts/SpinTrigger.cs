using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpinTrigger : MonoBehaviour {

	[SerializeField] Rigidbody car;
	private int torqueCount = 0;
	private int randomDirection = 1;

	void Start() {
	}

	void Update() {
		if (torqueCount > 0) {
			car.AddTorque(Vector3.up * 800000000 * randomDirection, ForceMode.Acceleration);
			torqueCount--;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Car") {
			System.Random r = new System.Random();
			randomDirection = r.Next(0, 2) == 0 ? 1 : -1;
			torqueCount = 50;
		}
	}
}