using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

    private string GO_TEXT = "GO!!!!!!!!!";
    private string GOAL_TEXT = "GGOOOOOAALLLLLLLL";
    private string GAME_OVER_TEXT = "Game Over";
    private string HIT_BALL_TEXT_CUSTOM_1 = "SSSSIIIICCCKKK";
    private string HIT_BALL_TEXT_CUSTOM_2 = "AWESOMEEEEE";

    [SerializeField] GameObject eventTextObject, carObject;
	[SerializeField] CountDown gameTimer;
	public float textScale;

	private Text eventText;
	private bool isCountdown, isGoal, isEndgame, isHitball;
	private int counting_down = 3;
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
	    /**
        * The below methods have to do with displaying the "Goal" goal text in the event text area
        */

   /**
        Public method to call to display the goal text
   */
	public void startGoalDisplay() {
		initGoal();
		isGoal = true;
	}

    /*
        Initializes event text settings
    */
	private void initGoal() {
		eventTextObject.SetActive(true);
		eventTextTransform.rotation = initialPosition;
		eventTextTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		eventText.text = GOAL_TEXT;
		textScale = 0;
      
		eventTextObject.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
	}

    /*
        Called every frame to update the animation
    */
	private void goalUpdateCall() {
		if (textScale < 0.015f) {
			textScale += 0.0001f;
			eventTextTransform.localScale += new Vector3(textScale, textScale, textScale);
		} else {
			eventTextObject.SetActive(false);
			isGoal = false;


		}
	}

        /**
        * The below methods have to do with displaying the "3,2,1 Go!" countdown text in the event text area
        */

    public void startCountdown() {

		 
		carScript.InputEnabled = false;
		eventTextObject.SetActive(true);
		updateCountdown();
		isCountdown = true;
	}

    /**
        Handles the animation of the countdown in the event text
    */
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

    /**
        Handles the changing text from 3 to 2 to 1 to go! in the countdown
    */
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

        /**
        * The below methods have to do with displaying the two custom texts that appear in event text area when hitting the ball quickly
        */


        /*
            Starts the custom text to appear on the screen
        */
    public void startRandomHitBallText(int whichText) {
		if (!isCustom1 || !isCustom2) {
			startRotation = eventTextTransform.rotation;
			if (whichText == 1) {
				initCustomHitball(1);
			} else if (whichText == 2) {
				initCustomHitball(2);
			}
			isHitball = true;
		}
	}

    /**
        Initializes the custom text
    */
    private void initCustomHitball(int whichCustomText)
    {
        isCustom1 = true;
        eventTextObject.SetActive(true);
        eventTextTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        switch (whichCustomText)
        {
            case 1:
                eventText.text = HIT_BALL_TEXT_CUSTOM_1;
                break;
            case 2:
                eventText.text = HIT_BALL_TEXT_CUSTOM_2;
                break;

        }
        textScale = 0;
    }
    /**
        Called once a frame to apply the animation
    */
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


	
}