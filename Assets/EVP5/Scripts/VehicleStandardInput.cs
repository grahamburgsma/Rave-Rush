//------------------------------------------------------------------------------------------------
// Edy's Vehicle Physics
// (c) Angel Garcia "Edy" - Oviedo, Spain
// http://www.edy.es
//------------------------------------------------------------------------------------------------

using UnityEngine;

namespace EVP
{

public class VehicleStandardInput : MonoBehaviour
	{
	public VehicleController target;

	public bool continuousForwardAndReverse = true;
	public string steerAxis = "Horizontal";
	public string throttleAndBrakeAxis = "Vertical";
	public string handbrakeAxis = "Jump";
	public KeyCode resetVehicleKey = KeyCode.Return;

	bool m_doReset = false;


	void OnEnable ()
		{
		// Cache vehicle

		if (target == null)
			target = GetComponent<VehicleController>();
		}


	void Update ()
		{
		if (target == null) return;

		if (Input.GetKeyDown(resetVehicleKey)) m_doReset = true;
		}


	void FixedUpdate ()
		{
		if (target == null) return;

		// Gather and process input

		float steerInput = Mathf.Clamp(Input.GetAxis(steerAxis), -1.0f, 1.0f);
		float forwardInput = Mathf.Clamp01(Input.GetAxis(throttleAndBrakeAxis));
		float reverseInput = Mathf.Clamp01(-Input.GetAxis(throttleAndBrakeAxis));
		float handbrakeInput = Mathf.Clamp01(Input.GetAxis(handbrakeAxis));

		float throttleInput = 0.0f;
		float brakeInput = 0.0f;

		if (continuousForwardAndReverse)
			{
			float minSpeed = 0.1f;
			float minInput = 0.1f;

			if (target.speed > minSpeed)
				{
				throttleInput = forwardInput;
				brakeInput = reverseInput;
				}
			else
				{
				if (reverseInput > minInput)
					{
					throttleInput = -reverseInput;
					brakeInput = 0.0f;
					}
				else
				if (forwardInput > minInput)
					{
					if (target.speed < -minSpeed)
						{
						throttleInput = 0.0f;
						brakeInput = forwardInput;
						}
					else
						{
						throttleInput = forwardInput;
						brakeInput = 0;
						}
					}
				}
			}
		else
			{
			bool reverse = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

			if (!reverse)
				{
				throttleInput = forwardInput;
				brakeInput = reverseInput;
				}
			else
				{
				throttleInput = -reverseInput;
				brakeInput = 0;
				}
			}

		// Apply input to vehicle

		target.steerInput = steerInput;
		target.throttleInput = throttleInput;
		target.brakeInput = brakeInput;
		target.handbrakeInput = handbrakeInput;

		// Do a vehicle reset

		if (m_doReset)
			{
			target.ResetVehicle();
			m_doReset = false;
			}
		}
	}
}