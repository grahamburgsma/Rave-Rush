using UnityEngine;
using System.Collections;

public class AntiRoll : MonoBehaviour {

	[SerializeField] WheelCollider WheelL, WheelR;
	[SerializeField] float antiRoll = 5000.0f;
	Rigidbody rigidbody = new Rigidbody();

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
	}


	void FixedUpdate() {
		WheelHit hit;
		float travelL = 0.0f, travelR = 0.0f;

		var groundedL = WheelL.GetGroundHit(out hit);
		if (groundedL)
			travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

		var groundedR = WheelR.GetGroundHit(out hit);
		if (groundedR)
			travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

		var antiRollForce = (travelL - travelR) * antiRoll;

		if (groundedL)
			rigidbody.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
				WheelL.transform.position); 
		if (groundedR)
			rigidbody.AddForceAtPosition(WheelR.transform.up * antiRollForce,
				WheelR.transform.position); 
	}
}

