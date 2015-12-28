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
		Rigidbody rB;

		// Use this for initialization
		void Start() {
			rB = GetComponent<Rigidbody>();
		}
	 
		//Visual updates - every frame
		void Update() {
			//Reset Car
			if (Input.GetKeyDown("r")) {
				transform.position = new Vector3(transform.position.x, 10, transform.position.z);
				transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
			}

			foreach (Wheel wheel in wheels) {
				wheel.wheelTransform.Rotate(Vector3.right * wheel.wheelCollider.rpm / 60 * 360 * Time.deltaTime);

				if (wheel.steer) {
					wheel.wheelTransform.localEulerAngles = new Vector3(wheel.wheelTransform.localEulerAngles.x, wheel.wheelCollider.steerAngle - wheel.wheelTransform.localEulerAngles.z, wheel.wheelTransform.localEulerAngles.z);
				}
			}
		}

		//Physics updates - more frequent
		void FixedUpdate() {
			float torque = Input.GetAxis("Vertical") * enginePower;
			float turnSpeed = Input.GetAxis("Horizontal") * turnPower * 2;
			float brake = Input.GetAxis("Jump") * brakePower;
			int jump = 0;

			foreach (Wheel wheel in wheels) {
				if (wheel.wheelCollider.isGrounded)
					jump++;
				
				if (wheel.handbrake)
					wheel.wheelCollider.brakeTorque = brake;

				if (wheel.power)
					wheel.wheelCollider.motorTorque = torque;

				if (wheel.steer)
					wheel.wheelCollider.steerAngle = turnSpeed;
			}

			if (Input.GetKeyDown("j")) {
				if (jump > 0)
					rB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
			}

			if (Input.GetKeyDown("n")) {
				rB.AddForce(transform.forward * nitroForce, ForceMode.Impulse);
			}

//			Vector3 relativeVelocity = transform.InverseTransformDirection(rB.velocity);
//			print(rB.velocity);
//			float restFactor = 0.2f;
//			if (Mathf.Abs(relativeVelocity.x) < restFactor)
//				rB.velocity = Vector3.left * 0;
//			if (Mathf.Abs(relativeVelocity.y) < restFactor)
//				rB.velocity = Vector3.up * 0;
//			if (Mathf.Abs(relativeVelocity.z) < restFactor)
//				rB.velocity = Vector3.right * 0;
		}
	}
}