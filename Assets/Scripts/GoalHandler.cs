﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalHandler : MonoBehaviour {

	[SerializeField] Text scoreText = null, scoreRedText = null, scoreGreenText = null, scoreBlueText = null, scoreYellowText = null, whereToScoreText = null;
	[SerializeField] GameObject arrow, ball, car, explosion, eventHandlerObject;

	[SerializeField] AudioClip goalScored_Sound = null, background_intro_sound = null, background_body_sound = null, countdown_sound = null;
	[SerializeField] AudioSource source;
	[SerializeField] CountDown gameTimer;

	public GoalTrigger redSide, blueSide, yellowSide, greenSide;
	public bool isDisco;

	private int totalScored, blueGoals, redGoals, yellowGoals, greenGoals;
	private bool endStarted;
	private EventHandler eHandler;
	private string whereToScore;

	// Use this for initialization
	void Start() {
		updateWhereToScore();
		eHandler = eventHandlerObject.GetComponent<EventHandler>();

		source.PlayOneShot(countdown_sound, 0.1f);

		if (isDisco) { 
			StartCoroutine(backgroundMusic());
		}

		eHandler.startCountdown();
	}

	IEnumerator backgroundMusic() {

		source.PlayOneShot(background_intro_sound, 0.1f);
		yield return new WaitForSeconds(background_intro_sound.length);
		if (gameTimer.timerSeconds > 0) {
            source.PlayOneShot(background_body_sound, 0.1f);
            yield return new WaitForSeconds(background_intro_sound.length);
		}

	}
   

	// Update is called once per frame
	void Update() {

		if (isDisco) {
			discoUpdate();
		} else {
			normalUpdate();
		}
		
	}

	void discoUpdate() {
        Color _sideColor = Color.red;

        if (gameTimer.endOfGame)
        {

            whereToScoreText.text = "Game Over";

            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            car.GetComponent<LittleRocketLeague.Car>().InputEnabled = false;

            if (gameTimer.timerSeconds <= 0)
            {
                SceneManager.LoadScene(0);
            }

        }
        else
        {
            switch (whereToScore)
            {
                case "Red":
                    if (redSide.goalsScored > 0)
                    {
                        redGoals++;
                        showExplosion(Color.red);
                        updateGoalsScored();

                    }
                    break;
                case "Blue":
                    if (blueSide.goalsScored > 0)
                    {
                        blueGoals++;
                        showExplosion(Color.blue);
                        updateGoalsScored();

                    }
                    break;
                case "Green":
                    if (greenSide.goalsScored > 0)
                    {
                        greenGoals++;
                        showExplosion(Color.green);
                        updateGoalsScored();

                    }
                    break;
                case "Yellow":
                    if (yellowSide.goalsScored > 0)
                    {
                        yellowGoals++;
                        showExplosion(Color.yellow);
                        updateGoalsScored();

                    }
                    break;
            }

            redSide.goalsScored = 0;
            blueSide.goalsScored = 0;
            greenSide.goalsScored = 0;
            yellowSide.goalsScored = 0;
        }
    }

	void normalUpdate() {
		Color _sideColor = Color.red;

		if (gameTimer.endOfGame) {

			whereToScoreText.text = "Game Over";

			ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			car.GetComponent<LittleRocketLeague.Car>().InputEnabled = false;

			if (gameTimer.timerSeconds <= 0) {
				SceneManager.LoadScene(0);
			}

		} else {
			switch (whereToScore) {
				case "Red":
					if (redSide.goalsScored > 0) {
						redGoals++;
						showExplosion(Color.red);
						updateGoalsScored();

					}
					break;
				case "Blue":
					if (blueSide.goalsScored > 0) {
						blueGoals++;
						showExplosion(Color.blue);
						updateGoalsScored();

					}
					break;
				case "Green":
					if (greenSide.goalsScored > 0) {
						greenGoals++;
						showExplosion(Color.green);
						updateGoalsScored();

					}
					break;
				case "Yellow":
					if (yellowSide.goalsScored > 0) {
						yellowGoals++;
						showExplosion(Color.yellow);
						updateGoalsScored();

					}
					break;
			}

			redSide.goalsScored = 0;
			blueSide.goalsScored = 0;
			greenSide.goalsScored = 0;
			yellowSide.goalsScored = 0;
		}
	}

	void updateGoalsScored() {
		//Set Goal event text
		eHandler.startGoalDisplay();

		source.PlayOneShot(goalScored_Sound);
		totalScored = redGoals + blueGoals + greenGoals + yellowGoals;
		scoreText.text = totalScored.ToString();
		scoreRedText.text = redGoals.ToString();
		scoreGreenText.text = greenGoals.ToString();
		scoreBlueText.text = blueGoals.ToString();
		scoreYellowText.text = yellowGoals.ToString();
		updateWhereToScore();
	}

    
	void updateWhereToScore() {
		string[] whereCanBeScored = new string[4] { "Red", "Green", "Blue", "Yellow" };
		System.Random r = new System.Random();
		int nextColour = r.Next(0, 4);
		whereToScore = whereCanBeScored[nextColour];
		whereToScoreText.text = "Score on : " + whereToScore;

    
		Renderer[] arrowRenderer = arrow.GetComponentsInChildren<Renderer>();
		Color _sideColor = Color.red;
		switch (whereToScore) {   //Look at this and tell me how you would do it, i could create it into one statement but i dont know whats better
            
			case "Red":
				foreach (Renderer component in arrowRenderer) {
					component.material.color = Color.red;
                    
				}
				break;
			case "Blue":
				foreach (Renderer component in arrowRenderer) {
					component.material.color = Color.blue;
                  
				}
				break;
			case "Green":
				foreach (Renderer component in arrowRenderer) {
					component.material.color = Color.green;
                  
				}
				break;
			case "Yellow":
				foreach (Renderer component in arrowRenderer) {
					component.material.color = Color.yellow;
                   
				}
				break;
		}

		ball.transform.position = Vector3.zero;
		makeBodyStayStill(ball.GetComponent<Rigidbody>());
	}

	void makeBodyStayStill(Rigidbody body) { 
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
	}

	void showExplosion(Color explosionColour) {
		Transform explosion_transform = explosion.GetComponent<Transform>();
		Transform ball_transform = ball.GetComponent<Transform>();
		explosion_transform.position = ball_transform.position;
		ParticleSystem explosion_particle = explosion.GetComponent<ParticleSystem>();
		explosion_particle.startColor = explosionColour;
		explosion_particle.Play();
	}
}
