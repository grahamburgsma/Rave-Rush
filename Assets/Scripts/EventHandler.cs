using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

	[SerializeField] GameObject eventTextObject, carObject;
	[SerializeField] CountDown gameTimer;
	public float textScale;

	private Text eventText;
	private bool isCountdown, isGoal, isEndgame, isHitball;
	private int counting_down = 3;

	private string GO_TEXT = "GO!!!!!!!!!";
	private string GOAL_TEXT = "GGOOOOOAALLLLLLLL";
	private string GAME_OVER_TEXT = "Game Over";
	private string HIT_BALL_TEXT_CUSTOM_1 = "SSSSIIIICCCKKK";
	private string HIT_BALL_TEXT_CUSTOM_2 = "AWESOMEEEEE";
	private Quaternion startRotation;
	private bool isCustom1, isCustom2;

	private LittleRocketLeague.Car carScript;

	Transform eventTextTransform;
	private Quaternion initialPosition;

	// Use this for initialization
	void Start() {
        
		eventText = eventTextObject.GetComponent<Text>();
		eventTextTransform = eventText.GetComponent<Transform>();
		carScript = carObject.GetComponent<LittleRocketLeague.Car>();
		initialPosition = eventTextTransform.rotation;

	}

	//need this otherwise start is never called? confused
	void OnEnable() {
		Start();
	}



	// Update is called once per frame
	void Update() {
		if (isCountdown) {
			countdownUpdateCall();
		}

		if (isGoal) {
			goalUpdateCall();
		}

		if (isHitball) {
			hitBallUpdateCall();
		}
        

	}
	/*
    ****************************************************    GOAL RELATED **********************
    */
	public void startGoalDisplay() {
		initGoal();
		isGoal = true;
	}

	private void initGoal() {
		eventTextObject.SetActive(true);
		eventTextTransform.rotation = initialPosition;
		eventTextTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		eventText.text = GOAL_TEXT;
		textScale = 0;
      
		eventTextObject.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
	}

	private void goalUpdateCall() {
		if (textScale < 0.015f) {
			textScale += 0.0001f;
			eventTextTransform.localScale += new Vector3(textScale, textScale, textScale);
		} else {
			eventTextObject.SetActive(false);
			isGoal = false;


		}
	}

	/*
    ****************************************************    COUNTDOWN RELATED **********************
    */

	public void startCountdown() {

		 
		carScript.InputEnabled = false;
		eventTextObject.SetActive(true);
		updateCountdown();
		isCountdown = true;
	}

	private void countdownUpdateCall() {
		if (eventText.text.Equals(GO_TEXT)) {
			if (textScale < 0.015f) {
				textScale += 0.0003f;
				eventTextTransform.localScale -= new Vector3(textScale, textScale, textScale);
			} else {
				updateCountdown();

			}
            
		} else {
			if (textScale < 0.009f && !eventText.text.Equals(GO_TEXT)) {
				textScale += 0.0001f;
				eventTextTransform.localScale -= new Vector3(textScale, textScale, textScale);
			} else {
				updateCountdown();
			}
		}
	}

	private void updateCountdown() {
		if (counting_down > 0) {
			textScale = 0;
			eventTextTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			eventText.text = counting_down.ToString();
			counting_down--;
		} else if (!eventText.text.Equals(GO_TEXT)) {
			carScript.InputEnabled = true;
			gameTimer.startGameClock();
			eventText.text = GO_TEXT;
			textScale = 0;
			eventTextTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		} else {
			eventTextObject.SetActive(false);
			isCountdown = false;
		}
	}

	/*
   ****************************************************    GAME OVER RELATED **********************
   */

	public void startEndGame() {
		initEndGame();
		isEndgame = true;
	}

	private void initEndGame() {
		eventTextObject.SetActive(true);

		eventTextTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		eventText.text = GAME_OVER_TEXT;
		textScale = 0;
	}

	private void endGameUpdateCall() {
		if (textScale < 0.015f) {
			textScale += 0.0001f;
			eventTextTransform.localScale += new Vector3(textScale, textScale, textScale);
		} else {
			eventTextObject.SetActive(false);
			isEndgame = false;
		}
	}

	/*
  ****************************************************    Random Event **********************
  */

	

	public void startRandomHitBallText(int whichText) {
		if (!isCustom1 || !isCustom2) {
			startRotation = eventTextTransform.rotation;
			if (whichText == 1) {
				initHitBall_sick();
			} else if (whichText == 2) {
				initCustomHitball2();
			}

			isHitball = true;
		}
	}

	private void initHitBall_sick() {
		isCustom1 = true;
		eventTextObject.SetActive(true);
		eventTextTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		eventText.text = HIT_BALL_TEXT_CUSTOM_1;
		textScale = 0;
		//eventTextObject.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
	}

   

	private void hitBallUpdateCall() {
		if (isCustom1) {
			if (textScale < 0.012f) {
				textScale += 0.0001f;
				eventTextTransform.localScale += new Vector3(textScale, textScale, textScale);

				eventTextTransform.Rotate(Vector3.left, 45.0f * Time.deltaTime);

			} else {
				eventTextObject.SetActive(false);
				isHitball = false;
				isCustom1 = false;
				eventTextTransform.rotation = startRotation;


			}
		} else if (isCustom2) {
			if (textScale < 0.01f) {
				textScale += 0.0001f;
				eventTextTransform.localScale += new Vector3(textScale, textScale, textScale);

				eventTextTransform.Rotate(Vector3.up, 50.0f * Time.deltaTime);

			} else {
				eventTextObject.SetActive(false);
				isHitball = false;
				isCustom2 = false;
				eventTextTransform.rotation = startRotation;
			}
		}
	}


	private void initCustomHitball2() {
		isCustom2 = true;
		eventTextObject.SetActive(true);
		eventTextTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		eventText.text = HIT_BALL_TEXT_CUSTOM_2;
		textScale = 0;
		//eventTextObject.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
	}
}