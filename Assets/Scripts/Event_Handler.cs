using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Event_Handler : MonoBehaviour {

    [SerializeField]
    GameObject event_object, car_object;

    private Text event_text; 

    public float size_count;

    private bool is_countdown, is_goal,is_endgame,is_hitball;
    private int counting_down = 3;

    private string go_text = "GO!!!!!!!!!";
    private string goal_text = "GGOOOOOAALLLLLLLL";
    private string game_over_text = "Game Over";
    private string hit_ball_text_sick = "SSSSIIIICCCKKK";
    private string hit_ball_text_awesome = "AWESOMEEEEE";

	private LittleRocketLeague.Car car_script;

    Transform event_transform;

	// Use this for initialization
	void Start () {
        event_text = event_object.GetComponent<Text>();
        event_transform = event_text.GetComponent<Transform>();
		car_script = car_object.GetComponent<LittleRocketLeague.Car>();
       
    }

    //need this otherwise start is never called? confused
    void OnEnable()
    {
        Start();
    }



    // Update is called once per frame
    void Update () {
        if (is_countdown)
        {
            countdownUpdateCall();
        }

        if (is_goal)
        {
            goalUpdateCall();
        }

        if (is_hitball){
            hitBallUpdateCall();
        }
        

    }
    /*
    ****************************************************    GOAL RELATED **********************
    */
    public void startGoalDisplay()
    {
        initGoal();
        is_goal = true;
    }

    private void initGoal()
    {
        event_object.SetActive(true);
        event_transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        event_text.text = goal_text;
        size_count = 0;
      
        event_object.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
    }

    private void goalUpdateCall()
    {
        if (size_count < 0.015f)
        {
            size_count += 0.0001f;
            event_transform.localScale += new Vector3(size_count, size_count, size_count);
        }
        else
        {
            event_object.SetActive(false);
            is_goal = false;


        }
    }

    /*
    ****************************************************    COUNTDOWN RELATED **********************
    */
    public void startCountdown()
    {

		 
		car_script.InputEnabled = false;
        event_object.SetActive(true);
        updateCountdown();
        is_countdown = true;
    }

    private void countdownUpdateCall() {

        if (size_count < 0.009f)
        {
            size_count += 0.0001f;
            event_transform.localScale -= new Vector3(size_count, size_count, size_count);
        }
        else
        {
            updateCountdown();


        }
    }

    private void updateCountdown()
    {
        
        if (counting_down > 0)
        {
            
            size_count = 0;
            event_transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            event_text.text = counting_down.ToString();
            counting_down--;
        }
        else if(!event_text.text.Equals(go_text))
        {
           
            event_text.text = go_text;
            size_count = 0;
            event_transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            event_object.SetActive(false);
            is_countdown = false;
			car_script.InputEnabled = true;
        }
    }

    /*
   ****************************************************    GAME OVER RELATED **********************
   */

    public void startEndGame()
    {
		
        initEndGame();
        is_endgame = true;
    }

    private void initEndGame()
    {
        event_object.SetActive(true);

        event_transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        event_text.text = game_over_text;
        size_count = 0;
    }

    private void endGameUpdateCall()
    {
        if (size_count < 0.015f)
        {
            size_count += 0.0001f;
            event_transform.localScale += new Vector3(size_count, size_count, size_count);
        }
        else
        {
            event_object.SetActive(false);
            is_endgame = false;


        }
    }

    /*
  ****************************************************    Random Event **********************
  */

    Quaternion start_rotation;
    bool is_sick,is_awesome;

    public void startRandomHitBallText(int whichText)
    {
        if (!is_sick || !is_awesome)
        {
            start_rotation = event_transform.rotation;
            if (whichText == 1)
            {
                initHitBall_sick();
            }
            else if (whichText == 2)
            {
                initHitBall_awesome();
            }

            is_hitball = true;
        }
    }

    private void initHitBall_sick()
    {
        is_sick = true;
        event_object.SetActive(true);
        event_transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        event_text.text = hit_ball_text_sick;
        size_count = 0;
        //event_object.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
    }

   

    private void hitBallUpdateCall()
    {
        if (is_sick)
        {
            if (size_count < 0.012f)
            {
                size_count += 0.0001f;
                event_transform.localScale += new Vector3(size_count, size_count, size_count);

                event_transform.Rotate(Vector3.left, 45.0f * Time.deltaTime);

            }
            else
            {
                event_object.SetActive(false);
                is_hitball = false;
                is_sick = false;
                event_transform.rotation = start_rotation;


            }
        }
        else if(is_awesome){
            if (size_count < 0.01f)
            {
                size_count += 0.0001f;
                event_transform.localScale += new Vector3(size_count, size_count, size_count);

                event_transform.Rotate(Vector3.up, 50.0f * Time.deltaTime);

            }
            else
            {
                event_object.SetActive(false);
                is_hitball = false;
                is_awesome = false;
                event_transform.rotation = start_rotation;


            }
        }
    }


    private void initHitBall_awesome()
    {
        is_awesome = true;
        event_object.SetActive(true);
        event_transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        event_text.text = hit_ball_text_awesome;
        size_count = 0;
        //event_object.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
    }

}


