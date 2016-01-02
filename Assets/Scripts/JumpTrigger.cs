using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JumpTrigger : MonoBehaviour {

	[SerializeField] Rigidbody car;

	void Start() {
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Car")
			car.AddForce(Vector3.up * 70000, ForceMode.Impulse);
	}
}