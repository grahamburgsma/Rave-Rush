using UnityEngine;
using System.Collections;
using System;

namespace LittleRocketLeague {

	[Serializable]
	public class Wheel {
		public WheelCollider wheelCollider;
		public Transform wheelTransform;
		public bool steer = false;
		public bool power = false;
		public bool brake = true;
		public bool handbrake = false;
	}

	public class Car : MonoBehaviour {

		[SerializeField] Wheel[] wheels = new Wheel[0];
		[SerializeField] float enginePower = 0, turnPower = 0, brakePower = 0, jumpForce = 40000, nitroForce = 15000;
		new Rigidbody rigidBody;
		new ConstantForce constantForce;

		private float torque = 0, turnSpeed = 0;

		// Use this for initialization
		void Start() {
			rigidBody = GetComponent<Rigidbody>();
			constantForce = GetComponent<ConstantForce>();
		}
	 
		//Visual updates - every frame
		void Update() {
			int numWheelsGrounded = 0;

			//Reset Car
			if (Input.GetKeyDown("r")) {
				transform.position = new Vector3(transform.position.x, 10, transform.position.z);
				transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
			}

			foreach (Wheel wheel in wheels) {
				if (wheel.wheelCollider.isGrounded)
					numWheelsGrounded++;
				
				wheel.wheelTransform.Rotate(Vector3.right * wheel.wheelCollider.rpm / 60 * 360 * Time.deltaTime);

				if (wheel.steer) {
					wheel.wheelTransform.localEulerAngles = new Vector3(wheel.wheelTransform.localEulerAngles.x, wheel.wheelCollider.steerAngle - wheel.wheelTransform.localEulerAngles.z, wheel.wheelTransform.localEulerAngles.z);
				}
			}
				
			//Forces
			if (numWheelsGrounded > 0) {
				constantForce.relativeForce = new Vector3(0, -2500, torque * 10);
				constantForce.relativeTorque = Vector3.up * turnSpeed * 4000;
			} else {
				constantForce.relativeForce = Vector3.down * 2500;
				constantForce.relativeTorque = Vector3.zero;
			}

			if (Input.GetKeyDown(KeyCode.J)) {
				if (numWheelsGrounded > 0) {
					rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
				}
			}

			if (Input.GetKeyDown(KeyCode.N)) {
				rigidBody.AddForce(transform.forward * nitroForce, ForceMode.Impulse);
			}
		}

		//Physics updates - more frequent
		void FixedUpdate() {
			torque = Input.GetAxis("Vertical") * enginePower;
			turnSpeed = Input.GetAxis("Horizontal") * turnPower * 2;
			float brake = Input.GetAxis("Jump") * brakePower;
			int numWheelsGrounded = 0;

			foreach (Wheel wheel in wheels) {
				if (wheel.wheelCollider.isGrounded)
					numWheelsGrounded++;
				
				if (wheel.handbrake)
					wheel.wheelCollider.brakeTorque = brake;

				if (wheel.power)
					wheel.wheelCollider.motorTorque = 0; //torque;

				if (wheel.steer)
					wheel.wheelCollider.steerAngle = turnSpeed;
			}
		}
	}
}