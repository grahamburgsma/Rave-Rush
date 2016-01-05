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
		[SerializeField] Transform COM;

		[Header("Driving & Steering")]
		[SerializeField] float turnFactor = 0;
		[SerializeField] float turnForce = 1000000;
		[SerializeField] float engineFactor = 0;
		[SerializeField] float engineForce = 100;
		[SerializeField] float brakeFactor = 0;

		[Header("Jump & Boost")]
		[SerializeField] float jumpForce = 40000;
		[SerializeField] float torqueForce = 1000000;
		[SerializeField] float nitroForce = 500000;

		[Header("Misc")]
		[SerializeField] float maxVelocity = 500;
		[SerializeField] float downForce = -2500;
			
		[Header("Sounds")]
		[SerializeField]AudioClip crash_Sound;
		[SerializeField]AudioClip ball_hit_Sound;
		[SerializeField]AudioClip jump_Sound,ball_hit_sick,ball_hit_awesome;
		[SerializeField]AudioSource source;

        [Header("Event Handler")]
        [SerializeField]GameObject eventHandlerObject;

        private Event_Handler eHandler;


        Rigidbody rigidBody;
		new ConstantForce constantForce;

       
		private float sqrMaxVelocity;
		private int torqueCount;

		// Use this for initialization
		void Start() {
            eHandler = eventHandlerObject.GetComponent<Event_Handler>();
           
            rigidBody = GetComponent<Rigidbody>();
			constantForce = GetComponent<ConstantForce>();

			sqrMaxVelocity = (float)Math.Pow(maxVelocity, 2);

//			rigidBody.centerOfMass = COM.position;
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
				
				wheel.wheelTransform.Rotate(Vector3.forward * wheel.wheelCollider.rpm / 60 * 360 * Time.deltaTime);

				if (wheel.steer) {
					wheel.wheelTransform.localEulerAngles = new Vector3(-wheel.wheelCollider.steerAngle, wheel.wheelTransform.localEulerAngles.y, wheel.wheelTransform.localEulerAngles.z);
				}
			}
				
			//Driving forces (helps give arcade style driving)
			if (numWheelsGrounded > 0) {
//				constantForce.relativeForce = new Vector3(0, downForce, Input.GetAxis("Vertical") * engineForce);
//				constantForce.relativeTorque = Vector3.up * Input.GetAxis("Horizontal") * turnForce;

//				rigidBody.AddRelativeForce(new Vector3(0, downForce, Input.GetAxis("Vertical") * engineForce), ForceMode.VelocityChange);
//				rigidBody.AddRelativeTorque(Vector3.up * Input.GetAxis("Horizontal") * turnForce);

				//Cancel torque forces if touches ground
				torqueCount = 0;
			} else {
//				constantForce.relativeForce = Vector3.up * downForce;
//				constantForce.relativeTorque = Vector3.zero;
			}

			//One torque isn't enough, repeat a few times
			if (torqueCount > 0) {
				Vector3 torqueVector = new Vector3(Input.GetAxis("Vertical") * torqueForce, 0, Input.GetAxis("Horizontal") * torqueForce * -1);
				rigidBody.AddRelativeTorque(torqueVector, ForceMode.Impulse);
				torqueCount--;
			}

			//Jump
			if (Input.GetKeyDown(KeyCode.J)) {
				if (numWheelsGrounded > 0) {
					rigidBody.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
					source.PlayOneShot(jump_Sound);
				} else {
					//rotate
					Vector3 torqueVector = new Vector3(Input.GetAxis("Vertical") * torqueForce, 0, Input.GetAxis("Horizontal") * torqueForce * -1);
					rigidBody.AddRelativeTorque(torqueVector, ForceMode.Impulse);


//					Vector3 targetAngles = rigidBody.transform.eulerAngles + 180f * Vector3.forward; // what the new angles should be
//					rigidBody.transform.eulerAngles = Mathf.LerpAngle(transform.eulerAngles, 180, 0.5 * Time.deltaTime);

//					float angle = Mathf.LerpAngle(transform.rotation.z, 90, Time.deltaTime * 0.5f);
//					transform.eulerAngles = new Vector3(0, 0, angle);

//					Quaternion toRotation = rigidBody.rotation;
//					toRotation.x += 180;
//					rigidBody.rotation = Quaternion.Slerp(rigidBody.rotation, toRotation, Time.deltaTime * 3.0f);

					//boost
//					Vector3 boostVector = new Vector3(Input.GetAxis("Horizontal") * nitroForce, 0, Input.GetAxis("Vertical") * nitroForce);
//					rigidBody.AddRelativeForce(boostVector, ForceMode.Impulse);
				}
			}

			//Nitro
			if (Input.GetKeyDown(KeyCode.N)) {
				rigidBody.AddRelativeForce(Vector3.forward * nitroForce, ForceMode.Impulse);
			}
		}

		//Physics updates - more frequent
		void FixedUpdate() {
			float driveSpeed = Input.GetAxis("Vertical") * engineFactor;
			float turnSpeed = Input.GetAxis("Horizontal") * turnFactor;
			float brakeSpeed = Input.GetAxis("Jump") * brakeFactor;
			int numWheelsGrounded = 0;

			foreach (Wheel wheel in wheels) {
				if (wheel.wheelCollider.isGrounded)
					numWheelsGrounded++;
				
				if (wheel.handbrake)
					wheel.wheelCollider.brakeTorque = brakeSpeed;

				if (wheel.power)
					wheel.wheelCollider.motorTorque = driveSpeed;

				if (rigidBody.velocity.magnitude > 150 && turnSpeed > 10)
					turnSpeed -= 5;

				if (wheel.steer)
					wheel.wheelCollider.steerAngle = turnSpeed;
			}

			if (numWheelsGrounded > 0) {
                constantForce.relativeForce = Vector3.up * downForce;

                rigidBody.AddRelativeTorque(Vector3.up * Input.GetAxis("Horizontal") * turnForce, ForceMode.Force);
                rigidBody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * engineForce, ForceMode.Force);
            } else {
                constantForce.relativeForce = Vector3.up * 0;
            }

			//max speed
			if (rigidBody.velocity.sqrMagnitude > sqrMaxVelocity) {
				rigidBody.velocity = rigidBody.velocity.normalized * maxVelocity;
			}
		}
        float last_ball_hit = 10.0f;
		void OnCollisionEnter(Collision collision) {
			if (collision.gameObject.tag == "Ball") {
                //last_ball_hit = Time.time;
                float current_hit = Time.time;
                float difference = current_hit - last_ball_hit;
                if (difference > 1.5f)
                {
                    System.Random r = new System.Random();
                    int coolText = r.Next(1, 10);
                    //int coolText = 1;
                    eHandler.startRandomHitBallText(coolText);

                    if (coolText == 1)
                    {
                        source.PlayOneShot(ball_hit_sick);
                    }
                    else if (coolText == 2)
                    { 
                        source.PlayOneShot(ball_hit_awesome);
                    }
                }
                last_ball_hit = current_hit;
				source.PlayOneShot(ball_hit_Sound);
			} else {
				source.PlayOneShot(crash_Sound);
			}

		}
	}
}