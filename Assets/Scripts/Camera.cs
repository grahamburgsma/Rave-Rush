using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public Transform car, ball;
	[SerializeField] float height = 5.0f, angleHeight = 9.0f, widthOffset = 0.0f, rotationDamping = 3.0f, heightDamping = 2.0f, zoomRatio = 0.2f, distance = 25.0f;
	private Vector3 rotationVector;
	private Transform lookObject;
	private bool ballCam = false;
	private float distanceChange;

	// Use this for initialization
	void Start() {
		distanceChange = distance;
		lookObject = car;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.B)) {
			if (ballCam)
				lookObject = car;
			else
				lookObject = ball;
			ballCam = !ballCam;
		}
	}

	void LateUpdate() {
		float wantedAngle = rotationVector.y;
		float wantedHeight = lookObject.position.y + angleHeight;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;

		myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
		myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
		var currentRotation = Quaternion.Euler(0, myAngle, 0);

		transform.position = car.position;
		transform.position -= currentRotation * Vector3.forward * distanceChange;
		transform.position = new Vector3(transform.position.x + widthOffset, myHeight, transform.position.z);

		if (!ballCam) {
			Vector3 lookAtLocation = car.transform.position;
			lookAtLocation.y += height;
			transform.LookAt(lookAtLocation);
		} else {
			Quaternion rotation = Quaternion.LookRotation(ball.position - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotation * Vector3.forward), rotationDamping * Time.deltaTime);
		}
	}

	void FixedUpdate() {
		Vector3 localVelocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);

		if (localVelocity.z < -1.5) {
			rotationVector.y = car.eulerAngles.y + 180;
		} else {
			rotationVector.y = car.eulerAngles.y;
		}
		float acc = car.GetComponent<Rigidbody>().velocity.magnitude;

		distanceChange = distance + acc * zoomRatio;
	}
}
