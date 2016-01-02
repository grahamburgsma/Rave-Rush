using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoalHandler : MonoBehaviour {




	[SerializeField] Text scoreText, scoreRedText, scoreGreenText, scoreBlueText, scoreYellowText, whereToScoreText;
	[SerializeField] Transform car;
	[SerializeField] GameObject arrow, ball;

	private Rigidbody carBody, ballBody;

	private string whereToScore;

	[SerializeField] AudioClip goalScored_Sound;
	[SerializeField] AudioSource source;

	public GoalTrigger redSide, blueSide, yellowSide, greenSide;
	public CountDown gameTimer;

	private int totalScored, blueGoals, redGoals, yellowGoals, greenGoals;

	// Use this for initialization
	void Start() {
		updateWhereToScore();   
	}
	
	// Update is called once per frame
	void Update() {

		if (gameTimer.timerSeconds <= 0) {
			whereToScoreText.text = "Game Over";
			ballBody = ball.GetComponent<Rigidbody>();
			carBody = car.GetComponent<Rigidbody>();
			makeBodyStayStill(ballBody);
			makeBodyStayStill(carBody);
		} else {
			switch (whereToScore) {
				case "Red":
					if (redSide.goalsScored > 0) {
						redSide.goalsScored = 0;
						redGoals++;
						updateGoalsScored();
					}
					break;
				case "Blue":
					if (blueSide.goalsScored > 0) {
						blueSide.goalsScored = 0;
						blueGoals++;
						updateGoalsScored();
					}
					break;
				case "Green":
					if (greenSide.goalsScored > 0) {
						greenSide.goalsScored = 0;
						greenGoals++;
						updateGoalsScored();
					}
					break;
				case "Yellow":
					if (yellowSide.goalsScored > 0) {
						yellowSide.goalsScored = 0;
						yellowGoals++;
						updateGoalsScored();
					}
					break;
			}
		}
        

	}

	void updateGoalsScored() {
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
		int nextColour = r.Next(0, 3);
		whereToScore = whereCanBeScored[nextColour];
		whereToScoreText.text = "Score on : " + whereToScore;

    
		Renderer[] arrowRenderer = arrow.GetComponentsInChildren<Renderer>();
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
		ballBody = ball.GetComponent<Rigidbody>();
		makeBodyStayStill(ballBody);
	}

	void makeBodyStayStill(Rigidbody body) { 
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
	}
}
