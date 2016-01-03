using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

	public WheelCollider wheelCollider;
	private Vector3 wheelCCenter;
	private RaycastHit hit;

	// Initialization
	void Start() {

	}

	void Update() {
		wheelCCenter = wheelCollider.transform.TransformPoint(wheelCollider.center);

		if (Physics.Raycast(wheelCCenter, -wheelCollider.transform.up, out hit, wheelCollider.suspensionDistance + wheelCollider.radius)) {
			transform.position = hit.point + (wheelCollider.transform.up * wheelCollider.radius);
		} else {
			transform.position = wheelCCenter - (wheelCollider.transform.up * wheelCollider.suspensionDistance);
		}

		Vector3 transformNew = transform.localPosition;
		transformNew.x += transform.localScale.x / 2;
		transform.localPosition = transformNew;
	}
}