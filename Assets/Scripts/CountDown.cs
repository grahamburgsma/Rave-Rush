using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour {

	public Text timerText;
	public int timerSeconds = 10;
	private AudioSource source;
	public AudioClip endRound_Sound;

	// Use this for initialization
	void Start() {
		setTime();
		InvokeRepeating("decreaseTimeLeft", 1.0f, 1.0f);
		source = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() {
       
        
	}

	void decreaseTimeLeft() {
		timerSeconds--;

		if (timerSeconds <= 0) {
			timerSeconds = 0;
			setTime();
			source.PlayOneShot(endRound_Sound);
			CancelInvoke("decreaseTimeLeft");
		}

		setTime();

	}

	void setTime() {
		TimeSpan timeSpan = TimeSpan.FromSeconds(timerSeconds);
		String timeText = string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
		timerText.text = timeText;
	}

}

