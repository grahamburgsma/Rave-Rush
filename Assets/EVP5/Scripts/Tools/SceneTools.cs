//------------------------------------------------------------------------------------------------
// Edy's Vehicle Physics
// (c) Angel Garcia "Edy" - Oviedo, Spain
// http://www.edy.es
//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace EVP
{

public class SceneTools : MonoBehaviour
	{
	public bool slowTimeMode = false;
	public float slowTime = 0.3f;

	public KeyCode hotkeyReset = KeyCode.R;
	public KeyCode hotkeyTime = KeyCode.T;

	// Use this for initialization
	void Start ()
		{

		}

	// Update is called once per frame
	void Update ()
		{
		if (Input.GetKeyDown(hotkeyReset)) Application.LoadLevel(0);
		if (Input.GetKeyDown(hotkeyTime)) slowTimeMode = !slowTimeMode;

		Time.timeScale = slowTimeMode? slowTime : 1.0f;
		}
	}
}