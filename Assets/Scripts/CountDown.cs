using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour {

	public Text timerText;
	public int timerSeconds = 10;
	private AudioSource source;
	public AudioClip endRoundSound;
	[SerializeField] bool isEndGameTimer;
	[SerializeField] GameObject car;

	public bool endOfGame;

	// Use this for initialization
	void Start() {
		source = GetComponent<AudioSource>();
		timerSeconds = PlayerPrefs.GetInt("GameLength");
		setTime();
	}

    /**
        Starts the game clock and makes it count down
    */
	public void startGameClock() {
		if (!isEndGameTimer) {
			InvokeRepeating("decreaseTimeLeft", 1.0f, 1.0f);
		}
	}

    /**
        Called repeatedly to decrease the timer every second
    */
	void decreaseTimeLeft() {
		timerSeconds--;
		if (!isEndGameTimer) {
			if (timerSeconds <= 0) {
				if (!endOfGame) {
					timerSeconds = 5;
					endOfGame = true;
					source.PlayOneShot(endRoundSound);
				}
			}
		}
		setTime();
	}
    

    /**
        Sets the timer text in the UI
    */
	void setTime() {
		if (!isEndGameTimer) {
			TimeSpan timeSpan = TimeSpan.FromSeconds(timerSeconds);
			String timeText = string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
			timerText.text = timeText;
		}
	}
}

