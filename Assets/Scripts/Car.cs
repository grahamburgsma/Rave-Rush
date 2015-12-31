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
		[SerializeField] float enginePower = 0, turnPower = 0, brakePower = 0, jumpForce = 40000, nitroForce = 500000, torqueRotate = 1000000;
		Rigidbody rigidBody;
		new ConstantForce constantForce;

		private float torque = 0, turnSpeed = 0;
		private int torqueCount;

		// Use this for initialization
		void Start() {
			rigidBody = GetComponent<Rigidbody>();
			constantForce = GetComponent<ConstantForce>();
		}
	 
		//Visual updates - every frame
		void Update() {
			int numWheelsGrounded = 0;


			//Reset Car
			if (Input.GetKeyDown(KeyCode.R)) {
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
				
			//Driving forces (helps give arcade style driving)
			if (numWheelsGrounded > 0) {
				constantForce.relativeForce = new Vector3(0, -5500, torque * 10);
				constantForce.relativeTorque = Vector3.up * turnSpeed * 4000;

				//Cancel torque forces if touches ground
				torqueCount = 0;
			} else {
				constantForce.relativeForce = Vector3.down * 2500;
				constantForce.relativeTorque = Vector3.zero;
			}

			//One torque isn't enough, repeat a few times
			if (torqueCount > 0) {
				Vector3 torqueVector = new Vector3(Input.GetAxis("Vertical") * torqueRotate, 0, Input.GetAxis("Horizontal") * torqueRotate * -1);
				rigidBody.AddRelativeTorque(torqueVector * torqueRotate, ForceMode.Impulse);
				torqueCount--;
			}

			//Jump
			if (Input.GetKeyDown(KeyCode.J)) {
				if (numWheelsGrounded > 0) {
					rigidBody.AddRelativeForce(transform.up * jumpForce, ForceMode.Impulse);
				} else {
					Vector3 torqueVector = new Vector3(Input.GetAxis("Vertical") * torqueRotate, 0, Input.GetAxis("Horizontal") * torqueRotate * -1);
					rigidBody.AddRelativeTorque(torqueVector * torqueRotate, ForceMode.Impulse);
					torqueCount = 25;
				}
			}

			//Nitro
			if (Input.GetKeyDown(KeyCode.N)) {
				rigidBody.AddRelativeForce(transform.forward * nitroForce, ForceMode.Impulse);
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

				if (rigidBody.velocity.magnitude > 150 && turnSpeed > 10)
					turnSpeed -= 5;

				if (wheel.steer)
					wheel.wheelCollider.steerAngle = turnSpeed;
			}
		}
	}
}