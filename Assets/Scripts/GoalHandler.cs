using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalHandler : MonoBehaviour {

	[SerializeField] Text scoreText = null, scoreRedText = null, scoreGreenText = null, scoreBlueText = null, scoreYellowText = null, whereToScoreText = null;
	[SerializeField] GameObject arrow, ball, car, explosion, eventHandlerObject;

	[SerializeField] AudioClip goalScored_Sound = null, background_intro_sound = null, background_body_sound = null, countdown_sound = null;
	[SerializeField] AudioSource source;
	[SerializeField] CountDown gameTimer;

	public GoalTrigger redSide, blueSide, yellowSide, greenSide;    //side triggers
	public bool isRave; //if its a rave

	private int totalScored, blueGoals, redGoals, yellowGoals, greenGoals;  //counters to keep track of the goals
	private bool endStarted;
	private EventHandler eHandler;
	private string whereToScore;

	// Use this for initialization
	void Start() {
		updateWhereToScore();
		eHandler = eventHandlerObject.GetComponent<EventHandler>();

		source.PlayOneShot(countdown_sound, 0.1f);

		if (isRave) { //only music when rave
			StartCoroutine(backgroundMusic());
		}

		eHandler.startCountdown();
	}


    /**
        Repeats the background music
    */
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

		if (isRave) {
			raveUpdate();
		} else {
			normalUpdate();
		}
		
	}

    /**
        Handles whether the game is over, and if a goal was scored.
    */
	void raveUpdate() {
        Color _sideColor = Color.red;

        if (gameTimer.endOfGame)    //if games over
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
        else  //Game isnt over, check to see if user scored
        {
            GoalTrigger _triggerColour;
            switch (whereToScore)
            {
                case "Red":
                    _triggerColour = getSideColour("red"); //gets which side is currently being shined on by the red light
                    if (_triggerColour.goalsScored > 0)
                    {
                        redGoals++;
                        showExplosion(Color.red);
                        updateGoalsScored();

                    }
                    break;
                case "Blue":
                    _triggerColour = getSideColour("blue");
                    if (_triggerColour.goalsScored > 0)
                    {
                        blueGoals++;
                        showExplosion(Color.blue);
                        updateGoalsScored();

                    }
                    break;
                case "Green":
                    _triggerColour = getSideColour("green");
                    if (_triggerColour.goalsScored > 0)
                    {
                        greenGoals++;
                        showExplosion(Color.green);
                        updateGoalsScored();

                    }
                    break;
                case "Yellow":
                    _triggerColour = getSideColour("yellow");
                    if (_triggerColour.goalsScored > 0)
                    {
                        yellowGoals++;
                        showExplosion(Color.yellow);
                        updateGoalsScored();

                    }
                    break;
            }

            redSide.goalsScored = 0;//reset all the goals 
            blueSide.goalsScored = 0;
            greenSide.goalsScored = 0;
            yellowSide.goalsScored = 0;
        }
    }

    /**
        Returns which side holds the given sideColour

        @param  sideColur   the colour of side you want to find
    */
    GoalTrigger getSideColour(string sideColour)
    {
        

        if (redSide.Colour.Equals(sideColour))
        {
            return redSide;
        }else if (blueSide.Colour.Equals(sideColour))
        {
            return blueSide;
        }
        else if (greenSide.Colour.Equals(sideColour))
        {
            return greenSide;
        }
        else if (yellowSide.Colour.Equals(sideColour))
        {
            return yellowSide;
        }

        return redSide;
    }


    /**
        Called to update the normal game (non rave)
    */
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

    /**
        Updates the goals scored text on the UI
    */
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

    /**
        Called after a goal is scored to determine where to score next
    */
	void updateWhereToScore() {
		string[] whereCanBeScored = new string[4] { "Red", "Green", "Blue", "Yellow" };
		System.Random r = new System.Random();
		int nextColour = r.Next(0, 4);
		whereToScore = whereCanBeScored[nextColour];
		whereToScoreText.text = "Score on : " + whereToScore;

    
		Renderer[] arrowRenderer = arrow.GetComponentsInChildren<Renderer>();
		Color _sideColor = Color.red;
		switch (whereToScore) {  
            
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

    /**
        Makes the given rigidbody stop moving

        @param  body    body to make stop moving
    */

	void makeBodyStayStill(Rigidbody body) { 
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
	}

    /**
        Shows an explosion where the goal was scored 

        @param  explosionColour What colour to make the explosion
    */
	void showExplosion(Color explosionColour) {
		Transform explosion_transform = explosion.GetComponent<Transform>();
		Transform ball_transform = ball.GetComponent<Transform>();
		explosion_transform.position = ball_transform.position;
		ParticleSystem explosion_particle = explosion.GetComponent<ParticleSystem>();
		explosion_particle.startColor = explosionColour;
		explosion_particle.Play();
	}
}
