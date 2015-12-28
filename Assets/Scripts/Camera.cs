using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public Transform car;
	[SerializeField] float distance = 25.0f, height = 9.0f, widthOffset = 0.0f, rotationDamping = 3.0f, heightDamping = 2.0f, zoomRatio = 0.2f, defaultDistance;
	Vector3 rotationVector;

	// Use this for initialization
	void Start () {
		defaultDistance = distance;
	}

	void LateUpdate () {
		float wantedAngle = rotationVector.y;
		float wantedHeight = car.position.y + height;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;
		myAngle = Mathf.LerpAngle(myAngle,wantedAngle,rotationDamping*Time.deltaTime);
		myHeight = Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);
		var currentRotation = Quaternion.Euler(0,myAngle,0);
		transform.position = car.position;
		transform.position -= currentRotation*Vector3.forward*distance;
		transform.position = new Vector3(transform.position.x + widthOffset, myHeight, transform.position.z);
		transform.LookAt(car);
	}

	void FixedUpdate (){
		Vector3 localVelocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);

		if (localVelocity.z < -1.5){
			rotationVector.y = car.eulerAngles.y + 180;
		} else {
			rotationVector.y = car.eulerAngles.y;
		}
		float acc = car.GetComponent<Rigidbody>().velocity.magnitude;
//		GetComponent<UnityEngine.Camera>().fieldOfView = DefaultFOV + acc*zoomRatio;

		distance = defaultDistance + acc * zoomRatio;
	}
}
