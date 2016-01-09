using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour {

	public Text timerText;
	public int timerSeconds = 10;
	private AudioSource source;
	public AudioClip endRound_Sound;
	[SerializeField] bool isEndGameTimer;
	[SerializeField] GameObject car;

	// Use this for initialization
	void Start() {

		source = GetComponent<AudioSource>();
        timerSeconds = PlayerPrefs.GetInt("GameLength");
        setTime();
	}


	public void startCounter() {
		timerSeconds = 6;
		InvokeRepeating("decreaseTimeLeft", 1.0f, 1.0f);
	}

    public void startGameClock()
    {
        if (!isEndGameTimer)
        {
           // timerSeconds = PlayerPrefs.GetInt("GameLength");
            InvokeRepeating("decreaseTimeLeft", 1.0f, 1.0f);
        }
    }

	void Update() {
       
        
	}

	void decreaseTimeLeft() {
		timerSeconds--;

		if (!isEndGameTimer) {
			if (timerSeconds <= 0) {
				timerSeconds = 0;
				setTime();
				source.PlayOneShot(endRound_Sound);
				CancelInvoke("decreaseTimeLeft");
			}
		}
		setTime();
        
	}

	void setTime() {
		if (!isEndGameTimer) {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timerSeconds);
			String timeText = string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
			timerText.text = timeText;
		}
	}
}

